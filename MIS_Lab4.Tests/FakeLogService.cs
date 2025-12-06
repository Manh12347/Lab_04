using MIS_Lab4.Services;

namespace MIS_Lab4.Tests
{
    public class FakeLogService : LogService
    {
        public override void WriteLog(string message)
        {
            // Bỏ trống để không ghi file khi chạy test
        }
    }
}

