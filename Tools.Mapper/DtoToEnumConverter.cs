﻿using System;
using AutoMapper;
using Tools.Application.DTOs;

namespace Tools.Mapper
{
    public class DtoToEnumConverter<TEnum> : ITypeConverter<EnumDTO<TEnum>, TEnum> where TEnum : struct, IConvertible
    {
        /// <summary>
        /// Convert an EnumDTO to the associated enum
        /// </summary>
        /// <param name="source">Source EnumDTO</param>
        /// <param name="destination">Destination</param>
        /// <param name="context">Context</param>
        /// <returns></returns>
        public TEnum Convert(EnumDTO<TEnum> source, TEnum destination, ResolutionContext context)
        {
            if (source?.Value != null)
            {
                return (TEnum) Enum.Parse(typeof(TEnum), source.Value, true);
            }
            else
            {
                //Return the first value of the enum
                return default;
            }
        }
    }
}