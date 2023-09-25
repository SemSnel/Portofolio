using System;
using System.Collections.Generic;
using System.Linq;

namespace SemSnel.Portofolio.Application.Common.Authorisations;

public class AuthorizationResult
{
    public bool IsAuthorized { get; set; }
    public List<string> Errors { get; set; } = new List<string>();
}