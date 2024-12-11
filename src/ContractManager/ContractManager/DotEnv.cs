namespace ContractManager
{
    public static class DotEnv
    {
        public static void Load(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return;
            }

            foreach (var line in File.ReadAllLines(filePath))
            {
                var firstEquals = line.IndexOf('=');

                if (firstEquals == -1)
                {
                    continue;
                }

                var variable = line.Substring(0, firstEquals);
                var value = line.Substring(firstEquals + 1);
                Environment.SetEnvironmentVariable(variable, value);
            }
        }
    }
}