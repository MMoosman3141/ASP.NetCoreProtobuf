using Google.Protobuf;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ASP.NetCoreProtobuf.Formatters {
  public class ProtobufJsonInputFormatter : TextInputFormatter {
    public ProtobufJsonInputFormatter() {
      SupportedMediaTypes.Clear();
      SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("application/json"));
      SupportedEncodings.Add(Encoding.UTF8);
      SupportedEncodings.Add(Encoding.Unicode);
    }

    public override async Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context, Encoding encoding) {
      HttpRequest request = context.HttpContext.Request;
      Type type = context.ModelType;
      IMessage protoType = (IMessage)Activator.CreateInstance(type);

      JsonParser.Settings settings = new JsonParser.Settings(100);
      JsonParser parser = new JsonParser(settings);

      using(TextReader reader = new StreamReader(request.Body, encoding)) {
        protoType = parser.Parse(reader, protoType.Descriptor);
      }

      return await InputFormatterResult.SuccessAsync(protoType);
    }

  }
}
