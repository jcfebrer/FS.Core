using Microsoft.VisualStudio.TestTools.UnitTesting;
using FSLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSLibrary.Tests
{
    [TestClass()]
    public class TextUtilTests
    {
        [TestMethod()]
        public void AsciiTest()
        {
            byte[] result = TextUtil.Ascii("A");
            if (result[0] != 65)
                Assert.Fail();
        }

        [TestMethod()]
        public void StartsWithTest()
        {
            bool result = TextUtil.StartsWith("esto es una prueba", "esto");
            if (!result)
                Assert.Fail();
        }

        [TestMethod()]
        public void EndsWithTest()
        {
            bool result = TextUtil.EndsWith("esto es una prueba", "prueba");
            if (!result)
                Assert.Fail();
        }

        [TestMethod()]
        public void LengthTest()
        {
            int result = TextUtil.Length("esto");
            if (result != 4)
                Assert.Fail();
        }

        [TestMethod()]
        public void InsertTest()
        {
            string result = TextUtil.Insert("esto es una prueba", 4, "hola");
            if (result != "estohola es una prueba")
                Assert.Fail();
        }

        [TestMethod()]
        public void InsertTest1()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void InsertTest2()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void TruncTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void AlinearIzqTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void AlinearDerTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void CenterTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void CenterTest1()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ReplicateTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void AmpliaTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void AmpliaTest1()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void SplitTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void SplitTest1()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ToLowerTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ToUpperTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void TrimStartTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void TrimEndTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void TrimTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void RemoveCharTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ContainsTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void CompareTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void CompareOnlyCharsTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void RemoveAccentsTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void CountStringTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void CountStringTest1()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetDelimitedTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void FilterDupTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void MaxCharTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void MinCharTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void SortStringAscTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void SortStringDescTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void CountWordsTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void CountWordsByBlankSpaceTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void StripControlCharsTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void StripControlCharsTest1()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetTokenIdxTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void QuitaEspaciosDupTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void CheckNumericTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void FilterStringTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void OnlyAlfaNumericTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void OnlyAlfaNumericWithAccentsTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void OnlyNumericTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void CountLinesTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetLineStringTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void JoinStringsTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void JoinStringsTest1()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void IsStringLowerTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void IsStringUpperTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void IsControlTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void IsDigitTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void IsLetterTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void IsLetterOrDigitTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void IsCharLowerTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void IsCharNumberTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void IsCharPuntuacionTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void IsCharSeparatorTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void IsCharSymbolTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void IsCharUpperTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void IsCharWhiteTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void CleanAmpersandTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void CleanQuotesTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void IsAlphaTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void CountDelimitedWordsTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void CountOccurrencesTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void EncloseStringTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GenerateRandomPasswordTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetDelimitedWordTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void NumberSuffixTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void TextToHTMLTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void CamelCaseTest()
        {
            string result = TextUtil.CamelCase("esto es una prueba");
            if (result != "EstoEsUnaPrueba")
                Assert.Fail();
        }

        [TestMethod()]
        public void ConvertCamelCaseTest()
        {
            string result = TextUtil.ConvertCamelCase("esto es una prueba");
            if (result != "EstoEsUnaPrueba")
                Assert.Fail();
        }

        [TestMethod()]
        public void ConvertReverseFullNameTest()
        {
            string result = TextUtil.ConvertReverseFullName("Febrer, Juan Carlos");
            if (result != "Juan Carlos Febrer")
                Assert.Fail();
        }

        [TestMethod()]
        public void InstrAfterTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void InstrLastTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void PermuteStringTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void PermuteStringTest1()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ReturnAllButTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void RandomStringTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ReplaceLastTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ReplaceArgsTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ReplaceMultiTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ReplaceREG2Test()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void UniqueWordsTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void FlipCaseTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void FormatValueTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void FormatValueTest1()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void IncrementStringTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void InstrTblTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void InstrTblTest1()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void InstrTblTest2()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void InstrTblRevTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void InstrTblRevTest1()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void InstrTblRevTest2()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void SearchStringTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void SearchStringTest1()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void SearchStringTest2()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void SearchStringTest3()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void StartsWithTest1()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void EndsWithTest1()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void PCaseTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void RemoveSpaceTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void RemoveInitialIlegalCharsTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void RemoveTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ToUTF8Test()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ToTitleCaseTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void CapitalizeTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ToCaseTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void RemoveIllegalCharTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void LikeTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void Num2SpanishPhraseTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void Num2SpanishPhraseTest1()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void PadLeftTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void PadLeftTest1()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void PadRightTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void PadRightTest1()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void StringCarTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void StrReverseTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void StrDupTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void IsEmailTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void RemoveHtmlTagsTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void RemoveExpressionSignalsTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void LastIndexOfTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void LastIndexOfTest1()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void IndexOfTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void IndexOfTest1()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void SubstringTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void SubstringTest1()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void LeftTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void RightTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void TranslateTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ReplaceTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void Replace_v2Test()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void RemoveIllegalDataTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void AdZeroTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ReplaceRecursiveTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ReplaceREGTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ReplaceREGTest1()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetHeadHtmlTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetDivHtmlTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void RemoveLinksTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void RemoveStylesTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void RemoveCommentsTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetWordCountTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetWordNumbTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void LinesInStringTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetLineFromStringTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void StringCountTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void StringCountTest1()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void PadCenterTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void PadCenterTest1()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void PadLeftTest2()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void PadLeftTest3()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void PadRightTest2()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void PadRightTest3()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ProperCaseTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void StringExtractTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void StringExtractTest1()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void StringExtractTest2()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void StringExtractTest3()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void StuffStringTest()
        {
            Assert.Fail();
        }
    }
}