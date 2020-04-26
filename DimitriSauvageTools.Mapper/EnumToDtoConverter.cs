using System;
using System.Globalization;
using AutoMapper;
using DimitriSauvageTools.Application.DTOs;
using DimitriSauvageTools.Helpers;

namespace DimitriSauvageTools.Mapper
{
    public class EnumToDtoConverter<TEnum> : ITypeConverter<TEnum, EnumDTO<TEnum>> where TEnum : struct, IConvertible
    {
        /// <summary>
        /// Convert an Enum to an EnumDTO
        /// </summary>
        /// <param name="source">Enum to convert</param>
        /// <param name="destination">Destination EnumDTO</param>
        /// <param name="context">Context</param>
        /// <returns></returns>
        public EnumDTO<TEnum> Convert(TEnum source, EnumDTO<TEnum> destination, ResolutionContext context)
        {
            return new EnumDTO<TEnum>(
                source.ToInt32(CultureInfo.InvariantCulture),
                source.ToString(),
                source.ToString().ToSentenceCase());
        }
    }
}