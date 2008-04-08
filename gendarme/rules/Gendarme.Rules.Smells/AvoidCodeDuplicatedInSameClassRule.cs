//
// Gendarme.Rules.Smells.AvoidCodeDuplicatedInSameClassRule class
//
// Authors:
//	Néstor Salceda <nestor.salceda@gmail.com>
//
// 	(C) 2007 Néstor Salceda
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//

using System;

using Mono.Cecil;
using Gendarme.Framework;
using Gendarme.Framework.Rocks;

namespace Gendarme.Rules.Smells {

	[Problem ("There is similar code in various methods in the same class.  Your code will be better if you can unify them.")]
	[Solution ("You should apply the Extract Method refactoring and invoke the method from the places.")]
	public class AvoidCodeDuplicatedInSameClassRule : Rule, ITypeRule {

		public RuleResult CheckType (TypeDefinition type)
		{
			// ignore code generated by compiler/tools, since they can generate some amount of duplicated code
			if (type.IsGeneratedCode ())
				return RuleResult.DoesNotApply;

			CodeDuplicatedLocator locator = new CodeDuplicatedLocator ();
			foreach (MethodDefinition current in type.Methods) {
				locator.CompareMethodAgainstTypeMethods (this, current, type);
				locator.CheckedMethods.Add (current.Name);
			}

			return Runner.CurrentRuleResult;
		}
	}
}
