from helps.database import DataBaseUtil
import MySQLdb

class User:
  
  def validate_user (username, password):
    has_acces = False
    db_connection = DataBaseUtil.get_connection()
    
    return has_acces
    

  def create_table ():
    is_created = False
    try:
      query =  "CREATE TABLE `users`"
      query += "("
      query += " `id_user` int NOT NULL AUTO_INCREMENT,"
      query += " `firstname` varchar(45) NOT NULL,"
      query += " `lastname` varchar(45) NOT NULL,"
      query += " `email` varchar(45) NOT NULL,"
      query += " `username` varchar(45) NOT NULL,"
      query += " `password` varchar(45) NOT NULL,"
      query += "  PRIMARY KEY (`id_user`)"
      query += ")"      
      query += "ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;"
      db_connection = DataBaseUtil.get_connection()

      #cursor = db_connection.cursor()
      #ecursor.execute()

    except MySQLdb.Error as e:
      print("Tabla, usuario no creada")
      print("Error " + e)
    return is_created
