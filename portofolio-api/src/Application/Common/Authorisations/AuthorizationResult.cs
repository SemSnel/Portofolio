using System;
using System.Collections.Generic;
using System.Linq;

namespace SemSnel.Portofolio.Application.Common.Authorisations;

public class AuthorizationResult
{
    public bool IsAuthorized { get; init; }
    public IEnumerable<string> Errors { get; init; } = Enumerable.Empty<string>();
}