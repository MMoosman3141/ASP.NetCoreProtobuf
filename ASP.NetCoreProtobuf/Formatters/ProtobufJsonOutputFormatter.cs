using Google.Protobuf;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ASP.NetCoreProtobuf.Formatters {
  public class ProtobufJsonOutputFormatter : TextOutputFormatter {
    public ProtobufJsonOutputFormatter() {
      SupportedMediaTypes.Clear();
      SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("application/json"));
      SupportedEncodings.Add(Encoding.UTF8);
      SupportedEncodings.Add(Encoding.Unicode);
    }

    public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding) {
      HttpResponse response = context.HttpContext.Response;

      IMessage protoObj = (IMessage)context.Object;

      JsonFormatter.Settings formatterSettings = new JsonFormatter.Settings(true);
      JsonFormatter jsonFormatter = new JsonFormatter(formatterSettings);

      await response.WriteAsync(jsonFormatter.Format(protoObj));
    }

  }
}
