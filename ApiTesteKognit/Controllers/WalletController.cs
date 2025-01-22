using Domain.Interfaces.Service;
using Microsoft.AspNetCore.Mvc;
using ViewModel.Wallet;

namespace ApiTesteKognit.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletController : ControllerBase
    {
        private readonly IWalletService _walletService;
        public WalletController(IWalletService walletService)
        {
            _walletService = walletService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateWallet([FromBody] WalletViewModel walletViewModel)
        {
            var result = await _walletService.CreateWalletAsync(walletViewModel);

            if (result.IsFailed)
                return BadRequest(result.Errors);

            return Ok(result.Value);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWallets()
        {
            var result = await _walletService.GetAllWalletsAsync();

            if (result == null || !result.Any())
                return NoContent();

            return Ok(result);
        }

        [HttpGet("GetWalletsByCpfUser/{nrCpf}")]
        public async Task<IActionResult> GetWalletsByCpfUserAsync(string nrCpf)
        {
            var result = await _walletService.GetWalletsByCpfUserAsync(nrCpf);

            if (result == null || !result.Any())
                return NoContent();

            return Ok(result);
        }
    }
}
