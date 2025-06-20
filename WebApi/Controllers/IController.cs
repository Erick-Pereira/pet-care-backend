using Microsoft.AspNetCore.Mvc;

namespace web_api.Controllers
{
    public interface IController<T>
    {
        Task<IActionResult> GetAll([FromQuery] int skip = 0, [FromQuery] int take = 100, [FromQuery] string? filter = null);

        Task<IActionResult> GetById(Guid id);

        Task<IActionResult> Create([FromBody] T request);

        Task<IActionResult> Update(Guid id, [FromBody] T request);

        Task<IActionResult> Delete(Guid id);

        Task<IActionResult> ToggleActive(Guid id);
    }
}