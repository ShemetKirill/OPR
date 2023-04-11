using System.Text;

namespace CourseOPR.Servises
{
    public class Parser
    {
        string path = "D:\\C#\\text.txt";
        public List<char> ArrayParse(string s)
        {
            var result = new List<char>();
            using (FileStream fstream = File.OpenRead(path))
            {
                // выделяем массив для считывания данных из файла
                byte[] buffer = new byte[fstream.Length];
                // считываем данные
                fstream.Read(buffer, 0, buffer.Length);
                // декодируем байты в строку
                string textFromFile = Encoding.Default.GetString(buffer);
                var splited = textFromFile.Split("\n");
                for (int i = 1; i < splited.Length; i += 2)
                {
                    var data = splited[i].Split(" ");
                    result.Add(data[0][9]);
                }
                return result;
            }

        }
    }
}
