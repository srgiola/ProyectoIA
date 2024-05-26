namespace SQLFreshRotten.api.LogicProcess.managmentfiles
{
    public class ManagmentFile
    {
        public List<string> GetContentFile (string fileName)
        {
            string path = Path.Combine(AppContext.BaseDirectory, "wwwroot", "files", fileName);
            if (!File.Exists(path))
                throw new Exception($"El archivo no existe en = {path}");

            List<string> linesOfData = new ();
            using (StreamReader reader = new StreamReader(path))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine() ?? string.Empty;
                    linesOfData.Add(line);
                }
            }

            return linesOfData;
        } 
    }
}
