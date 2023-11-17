using SemSnel.Portofolio.Domain._Common.Monads.ErrorOr;
using SemSnel.Portofolio.Domain._Common.Monads.Result;

namespace SemSnel.Portofolio.Domain._Common.TypeAliases;

using System;
using System.Collections.Generic;

/// <summary>
/// A base class for mapping identifiers to types and vice versa.
/// </summary>
/// <typeparam name="TIdentifier"> The identifier type. </typeparam>
/// <typeparam name="TType"> The type. </typeparam>
public abstract class BaseTypeIdentifierMap<TIdentifier, TType>
{
    private readonly IReadOnlyDictionary<TIdentifier, Type> _identifierToType;
    private readonly IReadOnlyDictionary<Type, TIdentifier> _typeToIdentifier;

    /// <summary>
    /// Initializes a new instance of the <see cref="BaseTypeIdentifierMap{TIdentifier,TType}"/> class.
    /// </summary>
    /// <param name="identifierToType"> The identifier to type mapping. </param>
    /// <param name="typeToIdentifier"> The type to identifier mapping. </param>
    /// <exception cref="ArgumentException"> Thrown when the mapping is invalid. </exception>
    protected BaseTypeIdentifierMap(
        IReadOnlyDictionary<TIdentifier, Type> identifierToType,
        IReadOnlyDictionary<Type, TIdentifier> typeToIdentifier)
    {
        _identifierToType = identifierToType;
        _typeToIdentifier = typeToIdentifier;

        var validation = Validate();

        if (validation.IsError)
        {
            throw new ArgumentException($"Invalid mapping: {validation.ToString()}");
        }
    }

    /// <summary>
    /// Gets the type from the identifier.
    /// </summary>
    /// <param name="identifier"> The identifier. </param>
    /// <returns> The type. </returns>
    /// <exception cref="KeyNotFoundException"> Thrown when the identifier is not found. </exception>
    public Type GetType(TIdentifier identifier)
    {
        if (_identifierToType.TryGetValue(identifier, out var fromIdentifier))
        {
            return fromIdentifier;
        }

        throw new KeyNotFoundException($"Identifier '{identifier}' not found.");
    }

    /// <summary>
    /// Gets the identifier from the type.
    /// </summary>
    /// <param name="type"> The type. </param>
    /// <returns> The identifier. </returns>
    /// <exception cref="KeyNotFoundException"> Thrown when the type is not found. </exception>
    public TIdentifier GetIdentifier(Type type)
    {
        if (_typeToIdentifier.TryGetValue(type, out var fromType))
        {
            return fromType;
        }

        throw new KeyNotFoundException($"Type '{type}' not found.");
    }

    /// <summary>
    /// Attempts to get the type from the identifier.
    /// </summary>
    /// <param name="identifier"> The identifier. </param>
    /// <param name="type"> The type. </param>
    /// <returns> True if the identifier was found, false otherwise. </returns>
    public bool TryGetType(TIdentifier identifier, out Type type)
    {
        if (_identifierToType.TryGetValue(identifier, value: out var value))
        {
            type = value;
            return true;
        }

        type = default!;

        return false;
    }

    /// <summary>
    /// Tries to get the identifier from the type.
    /// </summary>
    /// <param name="type"> The type. </param>
    /// <param name="identifier"> The identifier. </param>
    /// <returns> True if the identifier was found, false otherwise. </returns>
    public bool TryGetIdentifier(Type type, out TIdentifier identifier)
    {
        if (_typeToIdentifier.TryGetValue(type, out var value))
        {
            identifier = value;

            return true;
        }

        identifier = default!;

        return false;
    }

    private ErrorOr<Success> Validate()
    {
        var errors = new List<Error>();

        var shouldBeType = typeof(TType);

        foreach (var (identifier, type) in _identifierToType)
        {
            if (!shouldBeType.IsAssignableFrom(type))
            {
                errors.Add(Error.Validation($"Type '{type}' is not assignable from '{shouldBeType}'."));
            }
        }

        foreach (var type in _identifierToType)
        {
            if (!shouldBeType.IsAssignableFrom(type.Value))
            {
                errors.Add(
                    Error.Validation($"Type '{type}' is not assignable from '{shouldBeType}'."));
            }
        }

        if (errors.Count > 0)
        {
            return errors;
        }

        return Result.Success;
    }
}
