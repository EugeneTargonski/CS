using System;
using System.Data.SqlClient;
using System.Data;
using System.Reflection;

namespace ClassToSql
{
    static public class Object2SQL
    {
        //Parameterization not used. User hav`nt access to property names.
        static private void ConvertFromSQLType(PropertyInfo property, Object ChangedObject, Object NewValue)
        {
            var PropertyType = property.PropertyType;
            if (PropertyType == typeof(String))
            {
                if (NewValue.GetType() == typeof(DBNull))
                    property.SetValue(ChangedObject, null);
                else
                    property.SetValue(ChangedObject, Convert.ToString(NewValue));
            }
            else if (PropertyType == typeof(Nullable<Int32>))
            {
                if (NewValue.GetType() == typeof(DBNull))
                    property.SetValue(ChangedObject, null);
                else
                    property.SetValue(ChangedObject, Convert.ToInt32(NewValue));
            }
            else if (PropertyType == typeof(Int32))
            {
                if (NewValue.GetType() == typeof(DBNull))
                    property.SetValue(ChangedObject, 0);
                else
                    property.SetValue(ChangedObject, Convert.ToInt32(NewValue));
            }
            else if (PropertyType == typeof(Nullable<Int64>))
            {
                if (NewValue.GetType() == typeof(DBNull))
                    property.SetValue(ChangedObject, null);
                else
                    property.SetValue(ChangedObject, Convert.ToInt64(NewValue));
            }
            else if (PropertyType == typeof(Int64))
            {
                if (NewValue.GetType() == typeof(DBNull))
                    property.SetValue(ChangedObject, 0);
                else
                    property.SetValue(ChangedObject, Convert.ToInt64(NewValue));
            }
            else if (PropertyType == typeof(Nullable<Decimal>))
            {
                if (NewValue.GetType() == typeof(DBNull))
                    property.SetValue(ChangedObject, null);
                else
                    property.SetValue(ChangedObject, Convert.ToDecimal(NewValue));
            }
            else if (PropertyType == typeof(Decimal))
            {
                if (NewValue.GetType() == typeof(DBNull))
                    property.SetValue(ChangedObject, 0.0m);
                else
                    property.SetValue(ChangedObject, Convert.ToDecimal(NewValue));
            }
            else if (PropertyType == typeof(Nullable<DateTime>))
            {
                if (NewValue.GetType() == typeof(DBNull))
                    property.SetValue(ChangedObject, null);
                else
                    property.SetValue(ChangedObject, Convert.ToDateTime(NewValue));
            }
            else if (PropertyType == typeof(DateTime))
            {
                if (NewValue.GetType() == typeof(DBNull))
                    property.SetValue(ChangedObject, DateTime.MinValue);
                else
                    property.SetValue(ChangedObject, Convert.ToDateTime(NewValue));
            }
            else if (PropertyType == typeof(Nullable<Double>))
            {
                if (NewValue.GetType() == typeof(DBNull))
                    property.SetValue(ChangedObject, null);
                else
                    property.SetValue(ChangedObject, Convert.ToDouble(NewValue));
            }
            else if (PropertyType == typeof(Double))
            {
                if (NewValue.GetType() == typeof(DBNull))
                    property.SetValue(ChangedObject, 0.0);
                else
                    property.SetValue(ChangedObject, Convert.ToDouble(NewValue));
            }
            else if (PropertyType == typeof(Nullable<Single>))
            {
                if (NewValue.GetType() == typeof(DBNull))
                    property.SetValue(ChangedObject, null);
                else
                    property.SetValue(ChangedObject, Convert.ToSingle(NewValue));
            }
            else if (PropertyType == typeof(Single))
            {
                if (NewValue.GetType() == typeof(DBNull))
                    property.SetValue(ChangedObject, 0f);
                else
                    property.SetValue(ChangedObject, Convert.ToSingle(NewValue));
            }
            else if (PropertyType == typeof(Nullable<Int16>))
            {
                if (NewValue.GetType() == typeof(DBNull))
                    property.SetValue(ChangedObject, null);
                else
                    property.SetValue(ChangedObject, Convert.ToInt16(NewValue));
            }
            else if (PropertyType == typeof(Int16))
            {
                if (NewValue.GetType() == typeof(DBNull))
                    property.SetValue(ChangedObject, 0);
                else
                    property.SetValue(ChangedObject, Convert.ToInt16(NewValue));
            }
            else if (PropertyType == typeof(Nullable<Byte>))
            {
                if (NewValue.GetType() == typeof(DBNull))
                    property.SetValue(ChangedObject, null);
                else
                    property.SetValue(ChangedObject, Convert.ToByte(NewValue));
            }
            else if (PropertyType == typeof(Byte))
            {
                if (NewValue.GetType() == typeof(DBNull))
                    property.SetValue(ChangedObject, 0);
                else
                    property.SetValue(ChangedObject, Convert.ToByte(NewValue));
            }
            else if (PropertyType == typeof(Nullable<Boolean>))
            {
                if (NewValue.GetType() == typeof(DBNull))
                    property.SetValue(ChangedObject, null);
                else
                    property.SetValue(ChangedObject, Convert.ToBoolean(NewValue));
            }
            else if (PropertyType == typeof(Boolean))
            {
                if (NewValue.GetType() == typeof(DBNull))
                    property.SetValue(ChangedObject, false);
                else
                    property.SetValue(ChangedObject, Convert.ToBoolean(NewValue));
            }
            else if (PropertyType == typeof(Nullable<Char>))
            {
                if (NewValue.GetType() == typeof(DBNull))
                    property.SetValue(ChangedObject, null);
                else
                    property.SetValue(ChangedObject, Convert.ToChar(NewValue));
            }
            else if (PropertyType == typeof(Char))
            {
                if (NewValue.GetType() == typeof(DBNull))
                    property.SetValue(ChangedObject, ' ');
                else
                    property.SetValue(ChangedObject, Convert.ToChar(NewValue));
            }
            else if (PropertyType == typeof(Byte[]))
            {
                if (NewValue.GetType() == typeof(DBNull))
                    property.SetValue(ChangedObject, null);
                else
                    property.SetValue(ChangedObject, (byte[])NewValue);
            }
            else
            {
                if (NewValue.GetType() == typeof(DBNull))
                    property.SetValue(ChangedObject, "");
                else
                    property.SetValue(ChangedObject, Convert.ToString(NewValue));
            }
        }
        static public void ObjectList(string ConnectionString, Type ReadedObjectType)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                string TableName = ReadedObjectType.Name.ToString();
                TableName += "Table";
                string sqlExpression = "SELECT * FROM " + TableName;
                SqlCommand command = new SqlCommand(sqlExpression, connection);

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    var properties = ReadedObjectType.GetProperties();
                    foreach (var property in properties)
                    {
                        Console.Write(property.Name + "\t");
                    }
                    Console.WriteLine("Id"); ;
                    while (reader.Read()) //read dataset by string
                    {
                        properties = ReadedObjectType.GetProperties();
                        foreach (var property in properties)
                        {
                            Console.Write(reader[property.Name] + "\t");
                        }
                        Console.WriteLine(reader["Id"]);
                    }
                }
                reader.Close();
            }
        }
        static public Object ObjectRead(string ConnectionString, Type ReadedObjectType, int id)
        {
            Object Readed = null;
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                string TableName = ReadedObjectType.Name.ToString();
                TableName += "Table";
                string sqlExpression = "SELECT * FROM " + TableName + " WHERE Id = @id";
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlParameter idParam = new SqlParameter("@id", id);
                command.Parameters.Add(idParam);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    Readed = Activator.CreateInstance(ReadedObjectType);
                    while (reader.Read())
                    {
                        var properties = ReadedObjectType.GetProperties();
                        foreach (var property in properties)
                        {
                            ConvertFromSQLType(property, Readed, reader[property.Name]);
                        }
                        var ObjectId = ReadedObjectType.GetField("id");
                        ObjectId.SetValue(Readed, (int)reader["id"]);
                    }
                }
                reader.Close();
            }
            return Readed;
        }
        static public void ObjectDelete(string ConnectionString, Type DeletedObjectType, int id)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                string TableName = DeletedObjectType.Name.ToString();
                TableName += "Table";

                string sqlExpression = "DELETE " + TableName + " WHERE Id = @id";
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlParameter idParam = new SqlParameter("@id", id);
                command.Parameters.Add(idParam);
                int number = command.ExecuteNonQuery();
            }
        }
        static public void ObjectSave(string ConnectionString, object SavedObject)
        {
            //Objects with id = 0 considered new, and the rest - existing
            int id = (int)(SavedObject.GetType().GetField("id").GetValue(SavedObject));
            if (id == 0)
            { NewObjectSave(ConnectionString, SavedObject); }
            else
            { OldObjectSave(ConnectionString, SavedObject, id); }
        }
        static public void OldObjectSave(string ConnectionString, object SavedObject, int id)
        {
            string TableName = SavedObject.GetType().Name.ToString();
            TableName += "Table";
            string sqlExpression = "UPDATE " + TableName + " SET ";
            string comma = "";
            var properties = SavedObject.GetType().GetProperties();
            foreach (var property in properties)
            {
                sqlExpression = sqlExpression + comma + property.Name + "=@" + property.Name;
                comma = ", ";
            }
            sqlExpression = sqlExpression + " WHERE Id=@id";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlParameter AddParam;
                foreach (var property in properties)
                {
                    AddParam = new SqlParameter("@" + property.Name, property.GetValue(SavedObject) ?? DBNull.Value);
                    command.Parameters.Add(AddParam);
                }
                AddParam = new SqlParameter("@id", id);
                command.Parameters.Add(AddParam);
                int number = command.ExecuteNonQuery();
            }
        }
        static public void NewObjectSave(string ConnectionString, object SavedObject)
        {
            string TableName = SavedObject.GetType().Name.ToString();
            TableName += "Table";
            string sqlExpression = "INSERT INTO " + TableName + " (";
            string comma = "";
            var properties = SavedObject.GetType().GetProperties();
            foreach (var property in properties)
            {
                sqlExpression = sqlExpression + comma + property.Name;
                comma = ",";
            }
            sqlExpression = sqlExpression + ") VALUES (";
            comma = "@";
            foreach (var property in properties)
            {
                sqlExpression = sqlExpression + comma + property.Name;
                comma = ",@";
            }
            sqlExpression = sqlExpression + ");SET @id=SCOPE_IDENTITY()";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlParameter AddParam;
                foreach (var property in properties)
                {
                    AddParam = new SqlParameter("@" + property.Name, property.GetValue(SavedObject) ?? DBNull.Value);
                    command.Parameters.Add(AddParam);
                }
                SqlParameter idParam = new SqlParameter
                {
                    ParameterName = "@id",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output // Output parameter
                };
                command.Parameters.Add(idParam);
                int number = command.ExecuteNonQuery();
            }
        }
        static public void CreateFields(string ConnectionString, Type TransferedType)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                //Create a structure the properties of a class in SQL 
                string TableName = TransferedType.Name.ToString();
                TableName += "Table";
                CreateTable(ConnectionString, TableName);
                foreach (PropertyInfo propinfo in TransferedType.GetProperties())
                {
                    AddColumns(ConnectionString, TableName, propinfo.Name, propinfo.PropertyType);
                }
                //END //Create a structure the properties of a class in SQL 
            }
        }
        static private string GetSQLType(Type PropertyType)
        {
            if (PropertyType == typeof(String)) { return "nvarchar(MAX)"; }
            else if (PropertyType == typeof(Nullable<Int32>)) { return "int"; }
            else if (PropertyType == typeof(Int32)) { return "int"; }
            else if (PropertyType == typeof(Nullable<Int64>)) { return "bigint"; }
            else if (PropertyType == typeof(Int64)) { return "bigint"; }
            else if (PropertyType == typeof(Nullable<Decimal>)) { return "decimal"; }
            else if (PropertyType == typeof(Decimal)) { return "decimal"; }
            else if (PropertyType == typeof(Nullable<DateTime>)) { return "datetime"; }
            else if (PropertyType == typeof(DateTime)) { return "datetime"; }
            else if (PropertyType == typeof(Nullable<Double>)) { return "float"; }
            else if (PropertyType == typeof(Double)) { return "float"; }
            else if (PropertyType == typeof(Nullable<Single>)) { return "real"; }
            else if (PropertyType == typeof(Single)) { return "real"; }
            else if (PropertyType == typeof(Nullable<Int16>)) { return "smallint"; }
            else if (PropertyType == typeof(Int16)) { return "smallint"; }
            else if (PropertyType == typeof(Nullable<Byte>)) { return "tinyint"; }
            else if (PropertyType == typeof(Byte)) { return "tinyint"; }
            else if (PropertyType == typeof(Nullable<Boolean>)) { return "bit"; }
            else if (PropertyType == typeof(Boolean)) { return "bit"; }
            else if (PropertyType == typeof(Nullable<Char>)) { return "nchar(1)"; }
            else if (PropertyType == typeof(Char)) { return "nchar(1)"; }
            else if (PropertyType == typeof(Byte[])) { return "varbinary(max)"; }
            else return "nvarchar(MAX)";
        }
        static public void AddColumns(string ConnectionString, string TableName, string PropertyName, Type PropertyType)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                //Type comparison
                string sqltype = GetSQLType(PropertyType);
                //End Type comparison
                try
                {
                    string sqlExpression = "ALTER TABLE " + TableName + " " +
                        "ADD " +
                        PropertyName + " " + sqltype + " NULL";
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    command.ExecuteNonQuery();
                    Console.WriteLine("Field " + PropertyName + " of type " + sqltype + " added");
                }
                catch (System.Data.SqlClient.SqlException e)
                {
                    Console.WriteLine(e.Message);
                }
                catch (Exception e) { throw e; }
            }
        }
        static public void CreateTable(string ConnectionString, string TableName)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                try
                {
                    string sqlExpression = "CREATE TABLE " + TableName + " " +
                        "(" +
                        "id int IDENTITY(1,1) NOT FOR REPLICATION, " +
                        "CONSTRAINT " + TableName + "_Id PRIMARY KEY (id)" +
                        ")";
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    command.ExecuteNonQuery();
                    Console.WriteLine("Table " + TableName + " added");
                }
                catch (System.Data.SqlClient.SqlException e)
                {
                    Console.WriteLine(e.Message);
                }
                catch (Exception e) { throw e; }
            }
        }
        static public void CreateBase(string ConnectionString, string BaseName)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                string sqlExpression = "CREATE DATABASE " + BaseName;
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                try
                {
                    command.ExecuteNonQuery();
                    Console.WriteLine("Base " + BaseName + " created");
                }
                catch (System.Data.SqlClient.SqlException e)
                {
                    Console.WriteLine(e.Message);
                }
                //catch (Exception e) { throw e; }
            }
        }
    }
}
