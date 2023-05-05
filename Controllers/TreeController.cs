﻿using BackendTestTask.Services;
using Microsoft.AspNetCore.Mvc;

namespace BackendTestTask.Controllers
{
    public class TreeController : ApiControllerBase
    {
        private readonly ITreeAndNodeRepository _repository;

        public TreeController(ITreeAndNodeRepository repository, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            _repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> Get(string name)
        {
            return await TryCatchResultAsync(() =>
            {
                return _repository.GetTreeAsync(name);
            });
        }
    }
}
