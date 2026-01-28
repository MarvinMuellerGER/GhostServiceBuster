#pragma warning disable CS9113 // Parameter is unread.

using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GhostServiceBuster.IntegrationTests.Testees;

public sealed class TestPageModel(IService3 service3) : PageModel;
