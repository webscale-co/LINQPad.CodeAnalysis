﻿using System.Collections;
using System.Drawing;
using BrightIdeasSoftware;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQPad.CodeAnalysis
{
    internal class SyntaxTriviaWrapper : SyntaxWrapper
    {
        private readonly SyntaxTrivia _trivia;

        public SyntaxTriviaWrapper(SyntaxTrivia trivia)
        {
            _trivia = trivia;
        }

        public override object GetSyntaxObject()
        {
            return _trivia;
        }

        public override bool CanExpand()
        {
            return false;
        }

        public override IEnumerable GetChildren()
        {
            return null;
        }

        public override Color GetColor()
        {
            return Color.Maroon;
        }

        public override string GetKind()
        {
            // C#
            Microsoft.CodeAnalysis.CSharp.SyntaxKind cSharpKind = Microsoft.CodeAnalysis.CSharp.CSharpExtensions.Kind(_trivia);
            if (cSharpKind != Microsoft.CodeAnalysis.CSharp.SyntaxKind.None)
            {
                return cSharpKind.ToString();
            }

            // Visual Basic
            Microsoft.CodeAnalysis.VisualBasic.SyntaxKind visualBasicKind = Microsoft.CodeAnalysis.VisualBasic.VisualBasicExtensions.Kind(_trivia);
            if (visualBasicKind != Microsoft.CodeAnalysis.VisualBasic.SyntaxKind.None)
            {
                return visualBasicKind.ToString();
            }

            return "None";
        }

        public override string GetTreeText()
        {
            return (_trivia.Token.LeadingTrivia.Contains(_trivia) ? "Lead: " : "Trail: ") + base.GetTreeText();
        }

        public override string GetGraphText()
        {
            string text = base.GetGraphText();
            if (_trivia.Token.LeadingTrivia.Contains(_trivia))
            {
                return text + " ...";
            }
            return "... " + text;
        }

        public override string GetSpan()
        {
            return _trivia.FullSpan.ToString();
        }

        public override string GetText()
        {
            return _trivia.ToString();
        }
    }
}
