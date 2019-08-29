using Google.Protobuf;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ASP.NetCoreProtobuf.Formatters {
  public class ProtobufOutputFormatter : OutputFormatter {
    private readonly List<MediaTypeHeaderValue> _supportedTypes = new List<MediaTypeHeaderValue>() {
      MediaTypeHeaderValue.Parse("application/x-protobuf"),
      MediaTypeHeaderValue.Parse("application/protobuf"),
      MediaTypeHeaderValue.Parse("application/octet-stream"),
      MediaTypeHeaderValue.Parse("application/vnd.google.protobuf"),
    };

    public ProtobufOutputFormatter() {
      SupportedMediaTypes.Clear();
      _supportedTypes.ForEach(mediaType => SupportedMediaTypes.Add(mediaType));
    }

    public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context) {
      HttpResponse response = context.HttpContext.Response;

      IMessage protoObj = (IMessage)context.Object;
      byte[] serialized = protoObj.ToByteArray();

      await response.Body.WriteAsync(serialized, 0, serialized.Length);
    }
  }
}
