﻿using HtmlTags;
using HtmlTags.Conventions;
using HtmlTags.Conventions.Elements;
using System;

namespace ProductScrapper.Extensions
{
    public class EnumDropDownModifier : IElementModifier
    {
        public bool Matches(ElementRequest token)
        {
            return token.Accessor.PropertyType.IsEnum;
        }

        public void Modify(ElementRequest request)
        {
            var enumType = request.Accessor.PropertyType;

            request.CurrentTag.RemoveAttr("type");
            request.CurrentTag.TagName("select");
            request.CurrentTag.AddClass("select2");
            foreach (var value in Enum.GetValues(enumType))
            {
                var optionTag = new HtmlTag("option")
                    .Value(value.ToString())
                    .Text(Enum.GetName(enumType, value));
                request.CurrentTag.Append(optionTag);
            }
        }
    }

}