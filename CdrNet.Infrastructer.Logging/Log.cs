namespace CdrNet.Infrastructer.Logging
{
    public class Log
    {

        public Log(string _logMesaji, LogTipi _logTipi)
        {
            logMesaji = _logMesaji;
            logTipi = _logTipi;
            logZamani = DateTime.Now;
        }
        public LogTipi logTipi;
        public string logMesaji;
        public DateTime logZamani;
    }
}