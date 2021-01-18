using System;
using System.Collections.Generic;
using iText.IO.Util;
using iText.StyledXmlParser.Css;
using iText.StyledXmlParser.Css.Resolve.Shorthand.Impl;
using iText.Test;
using iText.Test.Attributes;

namespace iText.StyledXmlParser.Css.Resolve.Shorthand {
    public class GapShorthandResolverTest : ExtendedITextTest {
        [NUnit.Framework.Test]
        public virtual void InitialOrInheritOrUnsetValuesTest() {
            IShorthandResolver resolver = new GapShorthandResolver();
            String initialShorthand = CommonCssConstants.INITIAL;
            IList<CssDeclaration> resolvedShorthand = resolver.ResolveShorthand(initialShorthand);
            NUnit.Framework.Assert.AreEqual(2, resolvedShorthand.Count);
            NUnit.Framework.Assert.AreEqual(CommonCssConstants.ROW_GAP, resolvedShorthand[0].GetProperty());
            NUnit.Framework.Assert.AreEqual(CommonCssConstants.INITIAL, resolvedShorthand[0].GetExpression());
            NUnit.Framework.Assert.AreEqual(CommonCssConstants.COLUMN_GAP, resolvedShorthand[1].GetProperty());
            NUnit.Framework.Assert.AreEqual(CommonCssConstants.INITIAL, resolvedShorthand[1].GetExpression());
            String inheritShorthand = CommonCssConstants.INHERIT;
            resolvedShorthand = resolver.ResolveShorthand(inheritShorthand);
            NUnit.Framework.Assert.AreEqual(2, resolvedShorthand.Count);
            NUnit.Framework.Assert.AreEqual(CommonCssConstants.ROW_GAP, resolvedShorthand[0].GetProperty());
            NUnit.Framework.Assert.AreEqual(CommonCssConstants.INHERIT, resolvedShorthand[0].GetExpression());
            NUnit.Framework.Assert.AreEqual(CommonCssConstants.COLUMN_GAP, resolvedShorthand[1].GetProperty());
            NUnit.Framework.Assert.AreEqual(CommonCssConstants.INHERIT, resolvedShorthand[1].GetExpression());
            String unsetShorthand = CommonCssConstants.UNSET;
            resolvedShorthand = resolver.ResolveShorthand(unsetShorthand);
            NUnit.Framework.Assert.AreEqual(2, resolvedShorthand.Count);
            NUnit.Framework.Assert.AreEqual(CommonCssConstants.ROW_GAP, resolvedShorthand[0].GetProperty());
            NUnit.Framework.Assert.AreEqual(CommonCssConstants.UNSET, resolvedShorthand[0].GetExpression());
            NUnit.Framework.Assert.AreEqual(CommonCssConstants.COLUMN_GAP, resolvedShorthand[1].GetProperty());
            NUnit.Framework.Assert.AreEqual(CommonCssConstants.UNSET, resolvedShorthand[1].GetExpression());
        }

        [NUnit.Framework.Test]
        public virtual void InitialWithSpacesTest() {
            IShorthandResolver resolver = new GapShorthandResolver();
            String initialWithSpacesShorthand = "  initial  ";
            IList<CssDeclaration> resolvedShorthand = resolver.ResolveShorthand(initialWithSpacesShorthand);
            NUnit.Framework.Assert.AreEqual(2, resolvedShorthand.Count);
            NUnit.Framework.Assert.AreEqual(CommonCssConstants.ROW_GAP, resolvedShorthand[0].GetProperty());
            NUnit.Framework.Assert.AreEqual(CommonCssConstants.INITIAL, resolvedShorthand[0].GetExpression());
            NUnit.Framework.Assert.AreEqual(CommonCssConstants.COLUMN_GAP, resolvedShorthand[1].GetProperty());
            NUnit.Framework.Assert.AreEqual(CommonCssConstants.INITIAL, resolvedShorthand[1].GetExpression());
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.StyledXmlParser.LogMessageConstant.UNKNOWN_PROPERTY, Count = 3)]
        public virtual void ContainsInitialOrInheritOrUnsetShorthandTest() {
            IShorthandResolver resolver = new GapShorthandResolver();
            String containsInitialShorthand = "10px initial ";
            NUnit.Framework.Assert.AreEqual(JavaCollectionsUtil.EmptyList(), resolver.ResolveShorthand(containsInitialShorthand
                ));
            String containsInheritShorthand = "inherit 10%";
            NUnit.Framework.Assert.AreEqual(JavaCollectionsUtil.EmptyList(), resolver.ResolveShorthand(containsInheritShorthand
                ));
            String containsUnsetShorthand = "0 unset";
            NUnit.Framework.Assert.AreEqual(JavaCollectionsUtil.EmptyList(), resolver.ResolveShorthand(containsUnsetShorthand
                ));
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.StyledXmlParser.LogMessageConstant.SHORTHAND_PROPERTY_CANNOT_BE_EMPTY, Count = 2)]
        public virtual void EmptyShorthandTest() {
            IShorthandResolver resolver = new GapShorthandResolver();
            String emptyShorthand = "";
            NUnit.Framework.Assert.AreEqual(JavaCollectionsUtil.EmptyList(), resolver.ResolveShorthand(emptyShorthand)
                );
            String shorthandWithSpaces = "    ";
            NUnit.Framework.Assert.AreEqual(JavaCollectionsUtil.EmptyList(), resolver.ResolveShorthand(shorthandWithSpaces
                ));
        }

        [NUnit.Framework.Test]
        public virtual void GapWithOneValidValueTest() {
            IShorthandResolver resolver = new GapShorthandResolver();
            String shorthand = "10px";
            IList<CssDeclaration> resolvedShorthand = resolver.ResolveShorthand(shorthand);
            NUnit.Framework.Assert.AreEqual(2, resolvedShorthand.Count);
            NUnit.Framework.Assert.AreEqual(CommonCssConstants.ROW_GAP, resolvedShorthand[0].GetProperty());
            NUnit.Framework.Assert.AreEqual("10px", resolvedShorthand[0].GetExpression());
            NUnit.Framework.Assert.AreEqual(CommonCssConstants.COLUMN_GAP, resolvedShorthand[1].GetProperty());
            NUnit.Framework.Assert.AreEqual("10px", resolvedShorthand[1].GetExpression());
        }

        [NUnit.Framework.Test]
        public virtual void GapWithOneInvalidValueTest() {
            IShorthandResolver resolver = new GapShorthandResolver();
            String shorthand = "10";
            IList<CssDeclaration> resolvedShorthand = resolver.ResolveShorthand(shorthand);
            // TODO DEVSIX-4933 resulting List shall be empty
            NUnit.Framework.Assert.AreEqual(2, resolvedShorthand.Count);
            NUnit.Framework.Assert.AreEqual(CommonCssConstants.ROW_GAP, resolvedShorthand[0].GetProperty());
            NUnit.Framework.Assert.AreEqual("10", resolvedShorthand[0].GetExpression());
            NUnit.Framework.Assert.AreEqual(CommonCssConstants.COLUMN_GAP, resolvedShorthand[1].GetProperty());
            NUnit.Framework.Assert.AreEqual("10", resolvedShorthand[1].GetExpression());
        }

        [NUnit.Framework.Test]
        public virtual void GapWithTwoValidValuesTest() {
            IShorthandResolver resolver = new GapShorthandResolver();
            String shorthand = "10px 15px";
            IList<CssDeclaration> resolvedShorthand = resolver.ResolveShorthand(shorthand);
            NUnit.Framework.Assert.AreEqual(2, resolvedShorthand.Count);
            NUnit.Framework.Assert.AreEqual(CommonCssConstants.ROW_GAP, resolvedShorthand[0].GetProperty());
            NUnit.Framework.Assert.AreEqual("10px", resolvedShorthand[0].GetExpression());
            NUnit.Framework.Assert.AreEqual(CommonCssConstants.COLUMN_GAP, resolvedShorthand[1].GetProperty());
            NUnit.Framework.Assert.AreEqual("15px", resolvedShorthand[1].GetExpression());
        }

        [NUnit.Framework.Test]
        public virtual void GapWithValidAndInvalidValuesTest() {
            IShorthandResolver resolver = new GapShorthandResolver();
            String shorthand = "10px 15";
            IList<CssDeclaration> resolvedShorthand = resolver.ResolveShorthand(shorthand);
            // TODO DEVSIX-4933 resulting List shall be empty
            NUnit.Framework.Assert.AreEqual(2, resolvedShorthand.Count);
            NUnit.Framework.Assert.AreEqual(CommonCssConstants.ROW_GAP, resolvedShorthand[0].GetProperty());
            NUnit.Framework.Assert.AreEqual("10px", resolvedShorthand[0].GetExpression());
            NUnit.Framework.Assert.AreEqual(CommonCssConstants.COLUMN_GAP, resolvedShorthand[1].GetProperty());
            NUnit.Framework.Assert.AreEqual("15", resolvedShorthand[1].GetExpression());
        }

        [NUnit.Framework.Test]
        public virtual void GapWithInvalidAndValidValuesTest() {
            IShorthandResolver resolver = new GapShorthandResolver();
            String shorthand = "10 15px";
            IList<CssDeclaration> resolvedShorthand = resolver.ResolveShorthand(shorthand);
            // TODO DEVSIX-4933 resulting List shall be empty
            NUnit.Framework.Assert.AreEqual(2, resolvedShorthand.Count);
            NUnit.Framework.Assert.AreEqual(CommonCssConstants.ROW_GAP, resolvedShorthand[0].GetProperty());
            NUnit.Framework.Assert.AreEqual("10", resolvedShorthand[0].GetExpression());
            NUnit.Framework.Assert.AreEqual(CommonCssConstants.COLUMN_GAP, resolvedShorthand[1].GetProperty());
            NUnit.Framework.Assert.AreEqual("15px", resolvedShorthand[1].GetExpression());
        }

        [NUnit.Framework.Test]
        public virtual void GapWithZeroNumberTest() {
            IShorthandResolver resolver = new GapShorthandResolver();
            String shorthand = "0 10px";
            IList<CssDeclaration> resolvedShorthand = resolver.ResolveShorthand(shorthand);
            NUnit.Framework.Assert.AreEqual(2, resolvedShorthand.Count);
            NUnit.Framework.Assert.AreEqual(CommonCssConstants.ROW_GAP, resolvedShorthand[0].GetProperty());
            NUnit.Framework.Assert.AreEqual("0", resolvedShorthand[0].GetExpression());
            NUnit.Framework.Assert.AreEqual(CommonCssConstants.COLUMN_GAP, resolvedShorthand[1].GetProperty());
            NUnit.Framework.Assert.AreEqual("10px", resolvedShorthand[1].GetExpression());
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.StyledXmlParser.LogMessageConstant.UNKNOWN_PROPERTY)]
        public virtual void GapWithThreeValuesTest() {
            IShorthandResolver resolver = new GapShorthandResolver();
            String shorthand = "10px 15px 20px";
            IList<CssDeclaration> resolvedShorthand = resolver.ResolveShorthand(shorthand);
            NUnit.Framework.Assert.AreEqual(JavaCollectionsUtil.EmptyList(), resolvedShorthand);
        }
    }
}
