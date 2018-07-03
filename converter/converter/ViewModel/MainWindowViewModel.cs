using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.Commands;
using System.Windows.Input;
using converter.Model;
using System.Windows.Data;
using System.Net;
using System.IO;
using System.Xml;
using System.Data;
using System.Text.RegularExpressions;
using converter.SqlDataBase;

namespace converter.ViewModel
{
    class ConstructorAbortedException : Exception { }
    class MainWindowViewModel
    {
        string connStr = @"Data Source=(local)\SQLEXPRESS;
                            Initial Catalog=Temp;
                            Integrated Security=True";
        bool iResult;
        Random rnd;

        private DataBase CalculateBase;

        Dictionary<String, String> exchangerate = new Dictionary<string, string>
        {
            {"RUB_RUB", "http://free.currencyconverterapi.com/api/v5/convert?q=RUB_RUB&compact=y"},
            {"RUB_EUR", "http://free.currencyconverterapi.com/api/v5/convert?q=RUB_EUR&compact=y"},
            {"RUB_USD", "http://free.currencyconverterapi.com/api/v5/convert?q=RUB_USD&compact=y"},
            {"EUR_EUR", "http://free.currencyconverterapi.com/api/v5/convert?q=EUR_EUR&compact=y"},
            {"EUR_RUB", "http://free.currencyconverterapi.com/api/v5/convert?q=EUR_RUB&compact=y"},
            {"EUR_USD", "http://free.currencyconverterapi.com/api/v5/convert?q=EUR_USD&compact=y"},
            {"USD_USD", "http://free.currencyconverterapi.com/api/v5/convert?q=USD_USD&compact=y"},
            {"USD_RUB", "http://free.currencyconverterapi.com/api/v5/convert?q=USD_RUB&compact=y"},
            {"USD_EUR", "http://free.currencyconverterapi.com/api/v5/convert?q=USD_EUR&compact=y"}
        };
        public MainWindowViewModel()
        {
            this.LoadCommand = new DelegateCommand<object>(this.OnLoad);
            this.DataRateViewModel = new DataRateViewModel();
            this.SaveCommand = new DelegateCommand<object>(this.OnSave);
            this.GetbyDateCommand = new DelegateCommand<object>(this.GetbyDate);
            rnd = new Random();
        }

        public ICommand LoadCommand { get; private set; }

        public ICommand SaveCommand { get; private set; }

        public ICommand GetbyDateCommand { get; private set; }

        public DataRateViewModel DataRateViewModel { get; set; }

        private void OnLoad(object obj)
        {
            foreach (KeyValuePair<string, string> item in exchangerate)
            {
                string xmlResult = null;

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(item.Value);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                using (StreamReader resStream = new StreamReader(response.GetResponseStream()))
                {
                    xmlResult = resStream.ReadToEnd();
                }
                string[] temp = Regex.Split(xmlResult, "val\":");
                switch (item.Key)
                {
                    case "RUB_RUB":
                        DataRateViewModel.DataRate.RUBtoRUB = temp[1].Trim('}');
                        break;
                    case "RUB_EUR":
                        DataRateViewModel.DataRate.RUBtoEUR = temp[1].Trim('}');
                        break;
                    case "RUB_USD":
                        DataRateViewModel.DataRate.RUBtoUSD = temp[1].Trim('}');
                        break;
                    case "EUR_EUR":
                        DataRateViewModel.DataRate.EURtoEUR = temp[1].Trim('}');
                        break;
                    case "EUR_RUB":
                        DataRateViewModel.DataRate.EURtoRUB = temp[1].Trim('}');
                        break;
                    case "EUR_USD":
                        DataRateViewModel.DataRate.EURtoUSD = temp[1].Trim('}');
                        break;
                    case "USD_USD":
                        DataRateViewModel.DataRate.USDtoUSD = temp[1].Trim('}');
                        break;
                    case "USD_RUB":
                        DataRateViewModel.DataRate.USDtoRUB = temp[1].Trim('}');
                        break;
                    case "USD_EUR":
                        DataRateViewModel.DataRate.USDtoEUR = temp[1].Trim('}');
                        break;
                    default:
                        break;
                }
            }
        }

        private void OnSave(object obj)
        {
            CalculateBase = new DataBase(connStr);
            if (!CalculateBase.IsDataBaseExist())
            {
                iResult = CalculateBase.CreateDataBase("Temp");
                if (!iResult)
                {
                    throw new ConstructorAbortedException();
                }
            }

            iResult = CalculateBase.CreateTableIsNotExist("Temp", "TempTable", "Id INTEGER NOT NULL CONSTRAINT PKeyBaseId PRIMARY KEY", "Date DATETIME", "RUBtoRUB CHAR(255)", "RUBtoEUR CHAR(255)", "RUBtoUSD CHAR(255)", "EURtoEUR CHAR(255)", "EURtoRUB CHAR(255)", "EURtoUSD CHAR(255)", "USDtoUSD CHAR(255)", "USDtoRUB CHAR(255)", "USDtoEUR CHAR(255)");
            if (!iResult)
            {
                throw new ConstructorAbortedException();
            }

            try
            {
                DataTable TableDS = CreateDSTable("DataSetDataTable");
                DataSet DSet = new DataSet("TempDS");
                DSet.Tables.Add(TableDS);
                CalculateBase.FillDataTableDataBase(DSet, "TempTable", "DataSetDataTable");


                DataRow[] foundRows = TableDS.Select();

                for (int i = 0; i < foundRows.Length; i++)
                {

                    if ((DateTime)foundRows[i]["Date"] == DateTime.Now.Date)
                    {
                        break;
                    }
                    else
                    {
                        InsertRowDStable(TableDS);
                        CalculateBase.UpdateDataTableDataBase(DSet, "TempTable", "DataSetDataTable");
                    }

                    if ((DateTime)foundRows[i]["Date"] < DateTime.Today.AddDays(-2))
                    {
                        TableDS.Rows[i].Delete();
                        CalculateBase.UpdateDataTableDataBase(DSet, "TempTable", "DataSetDataTable");
                    }
                    else
                    {

                    }
                }

                //for (int i = 0; i < foundRows.Length; i++)
                //{

                //    if ((DateTime)foundRows[i]["Date"] < DateTime.Today.AddDays(-2))
                //    {
                //        TableDS.Rows[i].Delete();
                //        CalculateBase.UpdateDataTableDataBase(DSet, "TempTable", "DataSetDataTable");
                //    }
                //    else
                //    {

                //    }
                //}


                DSet.Clear();
                TableDS.Clear();
            }

            catch (FormatException ex)
            {
                if (ex.Source != null)
                    //System.Windows.MessageBox.Show("Date is null!Full the field!");
                    System.Windows.MessageBox.Show(ex.ToString(), "Date format ERROR!Try again!");
            }
            catch (FileNotFoundException ex)
            {
                if (ex.Source != null)
                    System.Windows.MessageBox.Show(ex.ToString(), "FileNotFoundException source:");
            }
            catch (IOException ex)
            {
                if (ex.Source != null)
                    System.Windows.MessageBox.Show(ex.ToString(), "IOException source:");
            }
        }


        private void GetbyDate(object obj)
        {

            CalculateBase = new DataBase(connStr);
            DateTime founddate;
            DateTime.TryParse(DataRateViewModel.DataRate.CurrentDay, out founddate);

            try
            {

                DataTable TableDS = CreateDSTable("DataSetDataTable");
                DataSet DSet = new DataSet("TempDS");
                DSet.Tables.Add(TableDS);
                CalculateBase.FillDataTableDataBase(DSet, "TempTable", "DataSetDataTable");

                DataRow[] foundRows = TableDS.Select();

                foreach (DataRow item in foundRows)
                {
                    if (item.Field<DateTime>("Date") == founddate)
                    {
                        DataRateViewModel.DataRate.RUBtoRUB = item.Field<string>("RUBtoRUB");
                        DataRateViewModel.DataRate.RUBtoEUR = item.Field<string>("RUBtoEUR");
                        DataRateViewModel.DataRate.RUBtoUSD = item.Field<string>("RUBtoUSD");
                        DataRateViewModel.DataRate.EURtoEUR = item.Field<string>("EURtoEUR");
                        DataRateViewModel.DataRate.EURtoRUB = item.Field<string>("EURtoRUB");
                        DataRateViewModel.DataRate.EURtoUSD = item.Field<string>("EURtoUSD");
                        DataRateViewModel.DataRate.USDtoUSD = item.Field<string>("USDtoUSD");
                        DataRateViewModel.DataRate.USDtoRUB = item.Field<string>("USDtoRUB");
                        DataRateViewModel.DataRate.USDtoEUR = item.Field<string>("USDtoEUR");
                    }
                }
                DSet.Clear();
                TableDS.Clear();
            }

            catch (FormatException ex)
            {
                if (ex.Source != null)
                    //System.Windows.MessageBox.Show("Date is null!Full the field!");
                    System.Windows.MessageBox.Show(ex.ToString(), "Date format ERROR!Try again!");
            }
            catch (FileNotFoundException ex)
            {
                if (ex.Source != null)
                    System.Windows.MessageBox.Show(ex.ToString(), "FileNotFoundException source:");
            }
            catch (IOException ex)
            {
                if (ex.Source != null)
                    System.Windows.MessageBox.Show(ex.ToString(), "IOException source:");
            }
        }


        private static DataTable CreateDSTable(string DataSetDataTableName)
        {
            DataTable dstable = new DataTable(DataSetDataTableName);

            DataColumn[] cols ={
                                  new DataColumn("Id",typeof(int)),
                                  new DataColumn("Date",typeof(DateTime)),
                                  new DataColumn("RUBtoRUB",typeof(string)),
                                  new DataColumn("RUBtoEUR",typeof(string)),
                                  new DataColumn("RUBtoUSD",typeof(string)),
                                  new DataColumn("EURtoEUR",typeof(string)),
                                  new DataColumn("EURtoRUB",typeof(string)),
                                  new DataColumn("EURtoUSD",typeof(string)),
                                  new DataColumn("USDtoUSD",typeof(string)),
                                  new DataColumn("USDtoRUB",typeof(string)),
                                  new DataColumn("USDtoEUR",typeof(string))
                              };

            dstable.Columns.AddRange(cols);
            dstable.Columns["Id"].AutoIncrement = true;
            dstable.Columns["Id"].AutoIncrementSeed = 1;
            dstable.Columns["Id"].AutoIncrementStep = 1;
            dstable.Columns["Id"].AllowDBNull = false;
            dstable.Columns["Id"].Unique = true;

            dstable.PrimaryKey = new DataColumn[] { dstable.Columns["Id"] };
            return dstable;
        }


        private void InsertRowDStable(DataTable dstable)
        {
            string date = null;
            //DateTime parsedate;
            ////date = string.Concat(projectWnd.EditWindow.Day, @"/", projectWnd.EditWindow.Month, @"/", projectWnd.EditWindow.Year);
            //if (!DateTime.TryParse(DataRateViewModel.DataRate.CurrentDay, out parsedate))
            //{
            //    throw new FormatException();
            //};

            DataRow row = dstable.NewRow();
            if (DateTime.Now.ToString() != string.Empty)
                row["Date"] = DateTime.Now.Day.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Year.ToString();
            row["RUBtoRUB"] = DataRateViewModel.DataRate.RUBtoRUB;
            row["RUBtoEUR"] = DataRateViewModel.DataRate.RUBtoEUR;
            row["RUBtoUSD"] = DataRateViewModel.DataRate.RUBtoUSD;
            row["EURtoEUR"] = DataRateViewModel.DataRate.EURtoEUR;
            row["EURtoRUB"] = DataRateViewModel.DataRate.EURtoRUB;
            row["EURtoUSD"] = DataRateViewModel.DataRate.EURtoUSD;
            row["USDtoUSD"] = DataRateViewModel.DataRate.USDtoUSD;
            row["USDtoRUB"] = DataRateViewModel.DataRate.USDtoRUB;
            row["USDtoEUR"] = DataRateViewModel.DataRate.USDtoEUR;
            dstable.Rows.Add(row);
        }

    }
}
