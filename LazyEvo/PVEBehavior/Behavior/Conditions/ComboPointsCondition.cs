﻿namespace LazyEvo.PVEBehavior.Behavior.Conditions
{
    using DevComponents.AdvTree;
    using DevComponents.DotNetBar;
    using DevComponents.Editors;
    using LazyLib.Wow;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Xml;

    internal class ComboPointsCondition : AbstractCondition
    {
        private IntegerInput valueInput;

        public ComboPointsCondition()
        {
            this.Condition = ConditionEnum.MoreThan;
            this.Value = 4;
        }

        private void CreateCondition(List<Node> re)
        {
            Node item = new Node {
                Text = "Combo Points "
            };
            item.Nodes.Add(base.CreateRadioButton("LessThan", "Less Than ", "ConditionEnum", this.Condition.Equals(ConditionEnum.LessThan)));
            item.Nodes.Add(base.CreateRadioButton("EqualTo", "Equal To", "ConditionEnum", this.Condition.Equals(ConditionEnum.EqualTo)));
            item.Nodes.Add(base.CreateRadioButton("MoreThan", "More Than", "ConditionEnum", this.Condition.Equals(ConditionEnum.MoreThan)));
            item.Expanded = true;
            re.Add(item);
        }

        private void CreateText(List<Node> re)
        {
            Node item = new Node {
                Text = "Info"
            };
            LabelX control = new LabelX {
                AutoSize = true,
                MaximumSize = new Size(300, 500),
                Text = "This condition will allow you to check the number of combopoints on your current target.",
                Visible = true,
                BackColor = Color.Transparent
            };
            item.Nodes.Add(base.CreateControl("Info", "Info", control));
            item.Expanded = true;
            re.Add(item);
        }

        private void CreateValue(List<Node> re)
        {
            Node item = new Node {
                Text = "value"
            };
            this.valueInput = new IntegerInput();
            this.valueInput.Value = this.Value;
            this.valueInput.ValueChanged += new EventHandler(this.IntegerInput_ValueChanged);
            item.Nodes.Add(base.CreateControl("Value", "Value", this.valueInput));
            item.Expanded = true;
            re.Add(item);
        }

        public override List<Node> GetNodes()
        {
            List<Node> re = new List<Node>();
            this.CreateCondition(re);
            this.CreateValue(re);
            this.CreateText(re);
            return re;
        }

        private void IntegerInput_ValueChanged(object sender, EventArgs e)
        {
            this.Value = this.valueInput.Value;
        }

        public override void LoadData(XmlNode xmlNode)
        {
            foreach (XmlNode node in xmlNode.ChildNodes)
            {
                if (node.Name.Equals("Condition"))
                {
                    this.Condition = (ConditionEnum) Enum.Parse(typeof(ConditionEnum), node.InnerText);
                    continue;
                }
                if (node.Name.Equals("Value"))
                {
                    this.Value = Convert.ToInt32(node.InnerText);
                }
            }
        }

        public override void NodeClick(Node node)
        {
            if ((node != null) && (node.Tag != null))
            {
                if (node.Tag.Equals("ConditionEnum"))
                {
                    this.Condition = (ConditionEnum) Enum.Parse(typeof(ConditionEnum), node.Name);
                }
                if (node.Tag.Equals("Value"))
                {
                    IntegerInput hostedControl = (IntegerInput) node.HostedControl;
                    this.Value = hostedControl.Value;
                }
            }
        }

        private ConditionEnum Condition { get; set; }

        private int Value { get; set; }

        public override string Name =>
            "Combo Points";

        public override string XmlName =>
            "ComboPointsCondition";

        public override string GetXML
        {
            get
            {
                object[] objArray = new object[] { "<Condition>" + this.Condition + "</Condition>", "<Value>", this.Value, "</Value>" };
                return string.Concat(objArray);
            }
        }

        public override bool IsOk =>
            !this.Condition.Equals(ConditionEnum.EqualTo) ? (!this.Condition.Equals(ConditionEnum.LessThan) ? (this.Condition.Equals(ConditionEnum.MoreThan) && (LazyLib.Wow.ObjectManager.MyPlayer.ComboPoints > this.Value)) : (LazyLib.Wow.ObjectManager.MyPlayer.ComboPoints < this.Value)) : (LazyLib.Wow.ObjectManager.MyPlayer.ComboPoints == this.Value);
    }
}

