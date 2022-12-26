using System;

namespace VDT.Core.Blazor.Wizard;

/// <summary>
///     Supplies information about a wizard finished event that is being raised because all steps of the wizard have been
///     completed
/// </summary>
public class WizardFinishedEventArgs : EventArgs
{
}

/// <summary>
///     Supplies information about a wizard started event that is being raised
/// </summary>
public class WizardStartedEventArgs : EventArgs
{
}

/// <summary>
///     Supplies information about a wizard attempting to complete a step event that is being raised
/// </summary>
public class WizardStepAttemptedCompleteEventArgs : EventArgs
{
    /// <summary>
    ///     Indicates if completion of the step should be cancelled; set to true if the wizard should not continue
    /// </summary>
    public bool IsCancelled { get; set; }
}

/// <summary>
///     Supplies information about a wizard step initialized event that is being raised
/// </summary>
public class WizardStepInitializedEventArgs : EventArgs
{
}

/// <summary>
///     Supplies information about a wizard stopped event that is being raised
/// </summary>
public class WizardStoppedEventArgs : EventArgs
{
}