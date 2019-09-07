using System;
using System.Collections.Generic;
using System.Linq;
using CoreNodeModels;
using Dynamo.Graph.Nodes;
using Dynamo.Utilities;
using Newtonsoft.Json;
using ProtoCore.AST.AssociativeAST;

using CSRegexOptions = System.Text.RegularExpressions.RegexOptions;


namespace RegularExpressions
{
    public abstract class RegExDropDownBase : DSDropDownBase
    {
        protected RegExDropDownBase(string value) : base(value) { }

        [JsonConstructor]
        public RegExDropDownBase(string value, IEnumerable<PortModel> inPorts, IEnumerable<PortModel> outPorts) : base(value, inPorts, outPorts) { }


        /// <summary>
        /// Whether it have valid Enumeration values to the output.
        /// </summary>
        /// <param name="itemValueToIgnore"></param>
        /// <param name="selectedValueToIgnore"></param>
        /// <returns>True is that there are valid values to output,false is that only a null value to output.</returns>
        public Boolean CanBuildOutputAst(string itemValueToIgnore = null, string selectedValueToIgnore = null)
        {
            if (Items.Count == 0 || SelectedIndex < 0)
                return false;
            if (!string.IsNullOrEmpty(itemValueToIgnore) && Items[0].Name == itemValueToIgnore)
                return false;
            if (!string.IsNullOrEmpty(selectedValueToIgnore) && Items[SelectedIndex].Name == selectedValueToIgnore)
                return false;
            return true;
        }
    }

    [NodeName("All Options")]
    [NodeDescription("Represents collection of Regular Expression options")]
    [NodeCategory("RegEx.Option")]
    [IsDesignScriptCompatible]
    public class FiltersByRule : RegExDropDownBase
    {
        private const string NO_OPTIONS = "No options available.";
        private const string outputName = "Regular expression option";

        public FiltersByRule() : base(outputName) { }

        [JsonConstructor]
        public FiltersByRule(IEnumerable<PortModel> inPorts, IEnumerable<PortModel> outPorts) : base(outputName, inPorts, outPorts) { }

        protected override SelectionState PopulateItemsCore(string currentSelection)
        {
            Items.Clear();

            var items = Enum.GetValues(typeof(CSRegexOptions));

            foreach (var option in items)
                Items.Add(new DynamoDropDownItem(option.ToString(), option.ToString()));

            Items = Items.OrderBy(x => x.Name).ToObservableCollection();
            return SelectionState.Restore;
        }        

        public override IEnumerable<AssociativeNode> BuildOutputAst(List<AssociativeNode> inputAstNodes)
        {
            if (!CanBuildOutputAst(NO_OPTIONS))
                return new[] { AstFactory.BuildAssignment(GetAstIdentifierForOutputIndex(0), AstFactory.BuildNullNode()) };

            var filterName = AstFactory.BuildStringNode((string)Items[SelectedIndex].Item);
            var functionCall = AstFactory.BuildFunctionCall(
                new Func<string, Option>(Option.ByName),
                new List<AssociativeNode> { filterName });

            return new[] { AstFactory.BuildAssignment(GetAstIdentifierForOutputIndex(0), functionCall) };
        }
    }
}