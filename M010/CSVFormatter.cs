using Microsoft.AspNetCore.Mvc.Formatters;
using System.Net.Mime;
using System.Reflection;
using System.Text;

namespace M010;

public class CSVFormatter : TextOutputFormatter
{
	public CSVFormatter()
	{
		SupportedMediaTypes.Add(MediaTypeNames.Text.Csv);
		SupportedEncodings.Add(Encoding.UTF8);
	}

	public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
	{
		IEnumerable<object> list = context.Object as IEnumerable<object>;
		Type t = list.First().GetType();

		StringBuilder sb = new();

		foreach (object o in list)
		{
			foreach (PropertyInfo p in t.GetProperties())
			{
				sb.Append(p.GetValue(o));
				sb.Append(";");
			}
			sb.Append(Environment.NewLine);
		}

		await context.HttpContext.Response.WriteAsync(sb.ToString());
	}
}
