using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Inventario.API.Shared
{
	public static class RequestHandler
	{
		public static async Task<IActionResult> HandleQuery<T>(T request, IMediator mediatR, ILogger log)
		{
			try
			{
				log.Debug("Ejecutando query del tipo {type} ", typeof(T).Name);
				return new OkObjectResult(await mediatR.Send(request));
			}
			catch (KeyNotFoundException ex)
			{
				return new NotFoundObjectResult(new
				{
					error = ex.Data
				});
			}
			catch (Exception e)
			{
				log.Error("Error ejecutando query del tipo {type}, Error: " + e.ToString(), typeof(T).Name);
				return new BadRequestObjectResult(new
				{
					error = e.Message,
					stackTrace = e.StackTrace
				});
			}
		}

		public static async Task<IActionResult> HandleCommand<T>(T request, IMediator mediatR, ILogger log)
		{
			try
			{
				log.Debug("Manejando la peticion HTTP del tipo {type} ", typeof(T).Name);
				var result = await mediatR.Send(request);
				if (result is Unit || result == null)
				{
					return new OkResult();
				}
				else
				{
					return new OkObjectResult(result);
				}
			}
			catch (Exception e)
			{
				log.Error("Error al manipular el comando del tipo {type}, Error: " + e.ToString(), typeof(T).Name);
				return new BadRequestObjectResult(new
				{
					error = e.Message,
					stackTrace = e.StackTrace
				});
			}
		}
	}
}
