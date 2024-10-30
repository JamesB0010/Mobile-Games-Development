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
    protected SuperBaseScriptableValRef valueToTest;

    [Tooltip("Choose a comparison operator from the dropdown menu")]
    [SerializeField]

    [SerializeReference]
    private ComparisonOperator comparisonOperator;

    public ComparisonOperator ComparisonOperator
    {
        get => this.comparisonOperator;

        set => this.comparisonOperator = value;
    }

    //Methods
    //getters
    public SuperBaseScriptableValRef ValueToTest
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
        return this.ComparisonOperator.Test((float)this.valueToTest.GetVal(), this.comparand);
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
        return this.ComparisonOperator.Test(Convert.ToBoolean(this.valueToTest.GetVal()), this.comparand);
    }
}