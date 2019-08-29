using Google.Protobuf;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ASP.NetCoreProtobuf.Formatters {
  public class ProtobufInputFormatter : InputFormatter {
    private readonly List<MediaTypeHeaderValue> _supportedTypes = new List<MediaTypeHeaderValue>() {
      MediaTypeHeaderValue.Parse("application/x-protobuf"),
      MediaTypeHeaderValue.Parse("application/protobuf"),
      MediaTypeHeaderValue.Parse("application/octet-stream"),
      MediaTypeHeaderValue.Parse("application/vnd.google.protobuf"),
    };

    public ProtobufInputFormatter() {
      SupportedMediaTypes.Clear();
      _supportedTypes.ForEach(mediaType => SupportedMediaTypes.Add(mediaType));
    }

    public override async Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context) {
      Type type = context.ModelType;
      HttpRequest request = context.HttpContext.Request;

      IMessage objMessage = (IMessage)Activator.CreateInstance(type);
      objMessage.MergeFrom(request.Body);

      return await InputFormatterResult.SuccessAsync(objMessage);
    }

  }
}
