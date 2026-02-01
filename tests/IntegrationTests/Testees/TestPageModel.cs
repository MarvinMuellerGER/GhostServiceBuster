#pragma warning disable CS9113 // Parameter is unread.

using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GhostServiceBuster.IntegrationTests.Testees;

public sealed class TestPageModel(IServiceInjectedIntoPageModel service3) : PageModel;
