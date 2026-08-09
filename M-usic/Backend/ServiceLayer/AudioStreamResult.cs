using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.ServiceLayer
{
    internal class AudioStreamResult
    {
        public Stream streamContent;
        public string contentType;
        public long contentLength;

        public AudioStreamResult(Stream stream, string contentType, long contentLength)
        {
            if(stream == null) 
                throw new ArgumentNullException("stream");
            this.streamContent = stream;
            this.contentType = contentType;
            this.contentLength = contentLength;
        }
    }
}
