using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows;
using System.Threading;
using System.Windows.Data;
using System.Data.SqlClient;


namespace converter.SqlDataBase
{
    public class DataBase
    {
        private string _ConStr;

        public DataBase(string ConStr)
        {
            _ConStr = ConStr;
        }
        public bool IsDataBaseExist()
        {

            using (SqlConnection conn = new SqlConnection(_ConStr))
            {
                try
                {
                    conn.Open();

                }
                catch (SqlException se)
                {
                    if (se.Number == 4060)
                        return false;
                }
                catch (System.Exception ex)
                {
                    if (ex.Source != null)
                        System.Windows.MessageBox.Show(ex.ToString(), "Database connect error!Try again!", MessageBoxButton.OK, MessageBoxImage.Information);
                    return false;
                }
                return true;
            }
        }


        public bool CreateDataBase(string createdbname)
        {
            if (!IsDataBaseExist())
            {
                using (SqlConnection conn = new SqlConnection(@"Data Source=(local)\SQLEXPRESS;
                                                       Integrated Security=True"))
                using (SqlCommand cmdSQLCommand = new SqlCommand(string.Format(string.Concat("CREATE DATABASE ", createdbname)), conn))
                {
                    conn.Open();

                    try
                    {
                        cmdSQLCommand.ExecuteNonQuery();
                        System.Windows.MessageBox.Show("DataBase is Created Successfully", "Ok", MessageBoxButton.OK, MessageBoxImage.Information);
                        Thread.Sleep(5000);
                        return true;
                    }
                    catch (System.Exception ex)
                    {
                        if (ex.Source != null)
                            System.Windows.MessageBox.Show(ex.ToString(), "Database create error!Try again!", MessageBoxButton.OK, MessageBoxImage.Information);
                    }

                }

            }
            return false;
        }

        public bool DropDataBase(string dropdbname)
        {
            if (IsDataBaseExist())
            {
                using (SqlConnection conn = new SqlConnection(@"Data Source=(local)\SQLEXPRESS;
                                                       Integrated Security=True"))
                using (SqlCommand cmdSQLCommand = new SqlCommand(string.Format(string.Concat("ALTER DATABASE ", dropdbname, " SET SINGLE_USER WITH ROLLBACK IMMEDIATE;", "DROP DATABASE ", dropdbname)), conn))
                {
                    conn.Open();

                    try
                    {
                        //conn.ChangeDatabase(dropdbname);
                        cmdSQLCommand.ExecuteNonQuery();
                        System.Windows.MessageBox.Show("DataBase drop Successfully", "Ok", MessageBoxButton.OK, MessageBoxImage.Information);
                        Thread.Sleep(5000);
                        return true;
                    }
                    catch (System.Exception ex)
                    {
                        if (ex.Source != null)
                            System.Windows.MessageBox.Show(ex.ToString(), "Database drop error!Try again!", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }

            }
            return false;
        }

        public bool CreateTableIsNotExist(string dbname, string tablename, string column1, string column2)
        {
            string SQLstring = "IF NOT EXISTS( SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = '" + tablename + "')" +
            " CREATE TABLE " + tablename + "(" + column1 + column2 + ")";

            using (SqlConnection conn = new SqlConnection(_ConStr))

            using (SqlCommand cmdSQLCommand = new SqlCommand(SQLstring, conn))
            {

                try
                {
                    conn.Open();
                    cmdSQLCommand.ExecuteNonQuery();
                    return true;
                }
                catch (SqlException ex)
                {
                    if (ex.Source != null)
                        System.Windows.MessageBox.Show(ex.ToString(), "Database create error!Try again!", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (System.Exception ex)
                {
                    if (ex.Source != null)
                        System.Windows.MessageBox.Show(ex.ToString(), "Fill DataBase Error!Try again!", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }

            return false;
        }

        public bool CreateTableIsNotExist(string dbname, string tablename, string column1, string column2, string column3)
        {
            string SQLstring = "IF NOT EXISTS( SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = '" + tablename + "')" +
            " CREATE TABLE " + tablename + "(" + column1 + column2 + column3 + ")";

            using (SqlConnection conn = new SqlConnection(_ConStr))
            using (SqlCommand cmdSQLCommand = new SqlCommand(SQLstring, conn))
            {
                try
                {
                    conn.Open();
                    cmdSQLCommand.ExecuteNonQuery();
                    return true;
                }
                catch (SqlException ex)
                {
                    if (ex.Source != null)
                        System.Windows.MessageBox.Show(ex.ToString(), "Database create error!Try again!", MessageBoxButton.OK, MessageBoxImage.Information);
                    //System.Environment.Exit(0);         
                }
                catch (System.Exception ex)
                {
                    if (ex.Source != null)
                        System.Windows.MessageBox.Show(ex.ToString(), "Fill DataBase Error!Try again!", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }

            return false;
        }

        public bool CreateTableIsNotExist(string dbname, string tablename, string column1, string column2, string column3, string column4)
        {
            string SQLstring = "IF NOT EXISTS( SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = '" + tablename + "')" +
            " CREATE TABLE " + tablename + "(" + column1 + column2 + column3 + column4 + ")";

            using (SqlConnection conn = new SqlConnection(_ConStr))
            using (SqlCommand cmdSQLCommand = new SqlCommand(SQLstring, conn))
            {
                try
                {
                    conn.Open();
                    cmdSQLCommand.ExecuteNonQuery();
                    return true;
                }
                catch (SqlException ex)
                {
                    if (ex.Source != null)
                        System.Windows.MessageBox.Show(ex.ToString(), "Database create error!Try again!", MessageBoxButton.OK, MessageBoxImage.Information);
                    //System.Environment.Exit(0);         
                }
                catch (System.Exception ex)
                {
                    if (ex.Source != null)
                        System.Windows.MessageBox.Show(ex.ToString(), "Fill DataBase Error!Try again!", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }

            return false;
        }

        public bool CreateTableIsNotExist(string dbname, string tablename, string column1, string column2, string column3, string column4, string column5, string column6)
        {
            string SQLstring = "IF NOT EXISTS( SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = '" + tablename + "')" +
            " CREATE TABLE " + tablename + "(" + column1 + ", " + column2 + ", " + column3 + ", " + column4 + ", " + column5 + ", " + column6 + ", " + ")";

            using (SqlConnection conn = new SqlConnection(_ConStr))
            using (SqlCommand cmdSQLCommand = new SqlCommand(SQLstring, conn))
            {
                try
                {
                    conn.Open();
                    cmdSQLCommand.ExecuteNonQuery();
                    return true;
                }
                catch (SqlException ex)
                {
                    if (ex.Source != null)
                        System.Windows.MessageBox.Show(ex.ToString(), "Database table create error!Try again!", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (System.Exception ex)

                {
                    if (ex.Source != null)
                        System.Windows.MessageBox.Show(ex.ToString(), "Database table create error!Try again!", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }

            return false;
        }
        public bool CreateTableIsNotExist(string dbname, string tablename, string column1, string column2, string column3, string column4, string column5, string column6, string column7)
        {
            string SQLstring = "IF NOT EXISTS( SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = '" + tablename + "')" +
            " CREATE TABLE " + tablename + "(" + column1 + "," + column2 + "," + column3 + "," + column4 + "," + column5 + "," + column6 + "," + column7 + ")";

            using (SqlConnection conn = new SqlConnection(_ConStr))
            using (SqlCommand cmdSQLCommand = new SqlCommand(SQLstring, conn))
            {
                try
                {
                    conn.Open();
                    cmdSQLCommand.ExecuteNonQuery();
                    return true;
                }
                catch (SqlException ex)
                {
                    if (ex.Source != null)
                        System.Windows.MessageBox.Show(ex.ToString(), "Database table create error!Try again!", MessageBoxButton.OK, MessageBoxImage.Information);
                    //System.Environment.Exit(0);         
                }
                catch (System.Exception ex)
                {
                    if (ex.Source != null)
                        System.Windows.MessageBox.Show(ex.ToString(), "Database table create error!Try again!", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }

            return false;
        }

        public bool CreateTableIsNotExist(string dbname, string tablename, string column1, string column2, string column3, string column4, string column5, string column6, string column7, string column8)
        {
            string SQLstring = "IF NOT EXISTS( SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = '" + tablename + "')" +
            " CREATE TABLE " + tablename + "(" + column1 + "," + column2 + "," + column3 + "," + column4 + "," + column5 + "," + column6 + "," + column7 + "," + column8 + ")";

            using (SqlConnection conn = new SqlConnection(_ConStr))
            using (SqlCommand cmdSQLCommand = new SqlCommand(SQLstring, conn))
            {
                try
                {
                    conn.Open();
                    cmdSQLCommand.ExecuteNonQuery();
                    return true;
                }
                catch (SqlException ex)
                {
                    if (ex.Source != null)
                        System.Windows.MessageBox.Show(ex.ToString(), "Database table create error!Try again!", MessageBoxButton.OK, MessageBoxImage.Information);
                    //System.Environment.Exit(0);         
                }
                catch (System.Exception ex)
                {
                    if (ex.Source != null)
                        System.Windows.MessageBox.Show(ex.ToString(), "Database table create error!Try again!", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }

            return false;
        }
        public bool CreateTableIsNotExist(string dbname, string tablename, string column1, string column2, string column3, string column4, string column5, string column6, string column7, string column8, string column9)
        {
            string SQLstring = "IF NOT EXISTS( SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = '" + tablename + "')" +
            " CREATE TABLE " + tablename + "(" + column1 + "," + column2 + "," + column3 + "," + column4 + "," + column5 + "," + column6 + "," + column7 + "," + column8 + "," + column9 + ")";

            using (SqlConnection conn = new SqlConnection(_ConStr))
            using (SqlCommand cmdSQLCommand = new SqlCommand(SQLstring, conn))
            {
                try
                {
                    conn.Open();
                    cmdSQLCommand.ExecuteNonQuery();
                    return true;
                }
                catch (SqlException ex)
                {
                    if (ex.Source != null)
                        System.Windows.MessageBox.Show(ex.ToString(), "Database table create error!Try again!", MessageBoxButton.OK, MessageBoxImage.Information);
                    //System.Environment.Exit(0);         
                }
                catch (System.Exception ex)
                {
                    if (ex.Source != null)
                        System.Windows.MessageBox.Show(ex.ToString(), "Database table create error!Try again!", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            return false;
        }


        public bool CreateTableIsNotExist(string dbname, string tablename, string column1, string column2, string column3, string column4, string column5, string column6, string column7, string column8, string column9, string column10, string column11)
        {
            string SQLstring = "IF NOT EXISTS( SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = '" + tablename + "')" +
            " CREATE TABLE " + tablename + "(" + column1 + "," + column2 + "," + column3 + "," + column4 + "," + column5 + "," + column6 + "," + column7 + "," + column8 + "," + column9 + "," + column10 + "," + column11 + ")";

            using (SqlConnection conn = new SqlConnection(_ConStr))
            using (SqlCommand cmdSQLCommand = new SqlCommand(SQLstring, conn))
            {
                try
                {
                    conn.Open();
                    cmdSQLCommand.ExecuteNonQuery();
                    return true;
                }
                catch (SqlException ex)
                {
                    if (ex.Source != null)
                        System.Windows.MessageBox.Show(ex.ToString(), "Database table create error!Try again!", MessageBoxButton.OK, MessageBoxImage.Information);
                    //System.Environment.Exit(0);         
                }
                catch (System.Exception ex)
                {
                    if (ex.Source != null)
                        System.Windows.MessageBox.Show(ex.ToString(), "Database table create error!Try again!", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }



            return false;
        }

        public bool InsertRowDataBase(string DataBaseTableName, string CommandString)
        {

            string SQLstring = "Insert Into " + DataBaseTableName + CommandString;

            using (SqlConnection conn = new SqlConnection(_ConStr))
            using (SqlCommand cmdSQLCommand = new SqlCommand(SQLstring, conn))
            {
                try
                {
                    conn.Open();
                    cmdSQLCommand.ExecuteNonQuery();
                    return true;
                }
                catch (SqlException ex)
                {
                    if (ex.Source != null)
                        System.Windows.MessageBox.Show(ex.ToString(), "Database create error!Try again!", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (System.Exception ex)
                {
                    if (ex.Source != null)
                        System.Windows.MessageBox.Show(ex.ToString(), "Fill DataBase Error!Try again!", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }

            return false;
        }

        public bool FillDataTableDataBase(DataSet myDataSet, string DataBaseTableName, string DataSetTableName)
        {
            int iResult;
            Object thisLock = new Object();
            using (SqlConnection conn = new SqlConnection(_ConStr))
            using (SqlDataAdapter dAdapt = new SqlDataAdapter("Select * From " + DataBaseTableName, _ConStr))
            using (SqlCommandBuilder builder = new SqlCommandBuilder(dAdapt))
            {
                try
                {
                    conn.Open();
                    Task FillDT = new Task(() => { lock (thisLock) { iResult = dAdapt.Fill(myDataSet, DataSetTableName); } });

                    FillDT.Start();
                    FillDT.Wait();
                    FillDT.Dispose();
                    return true;
                }
                catch (System.Exception ex)
                {
                    if (ex.Source != null)
                        System.Windows.MessageBox.Show(ex.ToString(), "Fill DataBase Error!Try again!", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            return false;
        }
        public bool FillDataTableDataBaseSelect(DataSet myDataSet, string DataBaseTableName, string DataSetTableName, string selectstring)
        {
            int iResult;
            Object thisLock = new Object();
            using (SqlConnection conn = new SqlConnection(_ConStr))
            using (SqlDataAdapter dAdapt = new SqlDataAdapter("Select * From " + DataBaseTableName, _ConStr))
            using (SqlCommandBuilder builder = new SqlCommandBuilder(dAdapt))
            {
                try
                {
                    conn.Open();
                    dAdapt.SelectCommand = new SqlCommand(selectstring, conn);
                    Task FillDT = new Task(() => { lock (thisLock) { iResult = dAdapt.Fill(myDataSet, DataSetTableName); } });

                    FillDT.Start();
                    FillDT.Wait();
                    FillDT.Dispose();
                    return true;
                }
                catch (System.Exception ex)
                {
                    if (ex.Source != null)
                        System.Windows.MessageBox.Show(ex.ToString(), "Fill DataBase Error!Try again!", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            return false;
        }

        public bool UpdateDataTableDataBase(DataSet myDataSet, string DataBaseTableName, string DataSetTableName)
        {
            int iResult;
            Object thisLock = new Object();
            using (SqlConnection conn = new SqlConnection(_ConStr))
            using (SqlDataAdapter dAdapt = new SqlDataAdapter("Select * From " + DataBaseTableName, _ConStr))
            using (SqlCommandBuilder builder = new SqlCommandBuilder(dAdapt))
            {
                try
                {
                    conn.Open();
                    Task FillDT = new Task(() => { lock (thisLock) { iResult = dAdapt.Update(myDataSet, DataSetTableName); } });

                    FillDT.Start();
                    FillDT.Wait();
                    FillDT.Dispose();
                    return true;
                }
                catch (System.Exception ex)
                {
                    if (ex.Source != null)
                        System.Windows.MessageBox.Show(ex.ToString(), "Update DataBase Error!Try again!", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            return false;
        }

        //public SqlDbType GetSQLType(Type CLRType)
        //{
        //    if (CLRType.Equals(typeof(string)))
        //        return SqlDbType.Char;
        //    else if (CLRType.Equals(typeof(Int32)))
        //        return SqlDbType.Int;
        //    else return SqlDbType.Variant;
        //}

        //public bool TestGetSQLType()
        //{
        //    int test = 10;
        //    System.Windows.Forms.MessageBox.Show(GetSQLType(test.GetType()).ToString());
        //    return true;

        //}

    }
}
