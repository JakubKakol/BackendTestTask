﻿using BackendTestTask.Data.DbContexts;
using BackendTestTask.Services;
using Microsoft.AspNetCore.Mvc;

namespace BackendTestTask.Controllers
{
    public class NodeController : ApiControllerBase
    {
        private readonly ITreeAndNodeRepository _repository;

        public NodeController(ITreeAndNodeRepository repository, IHttpContextAccessor httpContextAccessor, ApplicationDbContext context) : base(httpContextAccessor, context)
        {
            _repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> Create(string treeName, int parentNodeId, string nodeName)
        {
            return await TryCatchOperationAsync(() =>
            {
                return _repository.CreateNodeAsync(treeName, parentNodeId, nodeName);
            });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string treeName, int nodeId)
        {
            return await TryCatchOperationAsync(() =>
            {
                return _repository.DeleteNodeAsync(treeName, nodeId);
            });
        }

        [HttpPost]
        public async Task<IActionResult> Rename(string treeName, int nodeId, string newNodeName)
        {
            return await TryCatchOperationAsync(() =>
            {
                return _repository.RenameNodeAsync(treeName, nodeId, newNodeName);
            });
        }

    }
}
