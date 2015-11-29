using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    interface MediaStorage
    {
        void Authenticate(String login, String password);

        List<OutputFile> GetFileList(Int32 maxCount);

        void GetFile(Int32 Id);
    }

    struct OutputFile
    {
        public Int64 Id;
        public String fileName;
    }
}
