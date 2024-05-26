from MySQLdb import _mysql

class DataBaseUtil:

  @staticmethod
  def get_connection ():
        try:
            db = _mysql.connect("localhost", "root", "root", "perceptron")
            db.query("select Version();")
        except _mysql.Error as e:
            print("Conexión a la base de datos fallida: ")
            return None