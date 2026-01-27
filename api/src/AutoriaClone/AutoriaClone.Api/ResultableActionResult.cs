using Microsoft.AspNetCore.Mvc;
using IResult = AutoriaClone.Domain.Results.Abstract.IResult;

namespace AutoriaClone.Api;

public class ResultableActionResult : ActionResult
{
    public ResultableActionResult(IResult result)
    {
        Result = result;
    }

    public IResult Result { get; }
}
