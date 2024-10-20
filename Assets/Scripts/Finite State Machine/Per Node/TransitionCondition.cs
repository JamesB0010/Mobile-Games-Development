using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[Serializable]
public abstract class TransitionConditionBase
{
    //Attributes
    [SerializeField]
    protected bool transitionHandledByAgent = false;

    [SerializeField]
    protected ScriptableObjectValueReference valueToTest;

    [Tooltip("Choose a comparison operator from the dropdown menu")]
    [SerializeField]
    public ComparisonOperator comparisonOperator { get; set; }

    //Methods
    //getters
    public ScriptableObjectValueReference ValueToTest
    {
        get => this.valueToTest;

        set
        {
            this.valueToTest = value;
        }
    }


    //Abstract methods
    public abstract object GetComparand();
    public abstract void SetComparand(object val);
    public abstract bool Evaluate();


    //Conversion Methods
    public FloatTransitionCondition CastToFloatCondition()
    {
        FloatTransitionCondition newObj = new FloatTransitionCondition();
        newObj.transitionHandledByAgent = this.transitionHandledByAgent;
        newObj.valueToTest = this.valueToTest;
        newObj.comparisonOperator = this.comparisonOperator;
        return newObj;
    }

    public BoolTransitionCondition CastToBoolCondition()
    {
        BoolTransitionCondition newObj = new BoolTransitionCondition();
        newObj.transitionHandledByAgent = this.transitionHandledByAgent;
        newObj.valueToTest = this.valueToTest;
        newObj.comparisonOperator = this.comparisonOperator;
        return newObj;
    }



}


[Serializable]
public class FloatTransitionCondition : TransitionConditionBase
{
    [SerializeField] private float comparand;

    public override object GetComparand()
    {
        return this.comparand;
    }
    public override void SetComparand(object val)
    {
        this.comparand = (float)val;
    }

    public override bool Evaluate()
    {
        return this.comparisonOperator.Test((float)this.valueToTest.GetValue(), this.comparand);
    }
}

[Serializable]
public class BoolTransitionCondition : TransitionConditionBase
{
    [SerializeField] private bool comparand;
    public override object GetComparand()
    {
        return this.comparand;
    }

    public override void SetComparand(object val)
    {
        this.comparand = Convert.ToBoolean(val);
    }

    public override bool Evaluate()
    {
        return this.comparisonOperator.Test((bool)this.valueToTest.GetValue(), this.comparand);
    }
}