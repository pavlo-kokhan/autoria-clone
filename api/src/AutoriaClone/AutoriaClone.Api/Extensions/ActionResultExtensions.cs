using Microsoft.AspNetCore.Mvc;
using IResult = AutoriaClone.Domain.Results.Abstract.IResult;

namespace AutoriaClone.Api.Extensions;

public static class ActionResultExtensions
{
    public static IActionResult ToActionResult(this IResult result)
        => new ResultableActionResult(result);
}
