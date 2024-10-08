﻿using Isopoh.Cryptography.Argon2;
using Microsoft.Extensions.Options;

namespace FjsGram.Data.Passwords;

internal sealed class ArgonPasswordManager(IOptionsSnapshot<ArgonOptions> options) : IPasswordManager
{
    public string HashPassword(string password) => Argon2.Hash(password);

    public bool VerifyPassword(string hashed, string input)
    {
        return Argon2.Verify(hashed, input, options.Value.Secret);
    }
}
