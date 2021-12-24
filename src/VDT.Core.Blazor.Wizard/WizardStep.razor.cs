﻿using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Threading.Tasks;

namespace VDT.Core.Blazor.Wizard {
    /// <summary>
    /// Step as part of a wizard
    /// </summary>
    public partial class WizardStep : ComponentBase {
        [CascadingParameter] private Wizard? Parent { get; set; }

        /// <summary>
        /// Content of a wizard step such as an explanation or an <see cref="EditForm"/>
        /// </summary>
        [Parameter] public RenderFragment? ChildContent { get; set; }

        /// <summary>
        /// A callback that will be invoked when this step is first rendered
        /// </summary>
        [Parameter]
        public EventCallback<InitializeStepEventArgs> OnInitializeStep { get; set; }

        /// <summary>
        /// A callback that will be invoked when the user tries to go to the next step
        /// </summary>
        [Parameter] public EventCallback<TryCompleteStepEventArgs> OnTryCompleteStep { get; set; }

        /// <summary>
        /// Indicates whether or not the wizard step is currently active
        /// </summary>
        public bool IsActive => Parent?.ActiveStep == this;

        /// <inheritdoc/>
        protected override void OnInitialized() {
            Parent?.AddStep(this);
        }
        
        internal async Task Initialize() {
            var args = new InitializeStepEventArgs();

            await OnInitializeStep.InvokeAsync(args);
        }

        internal virtual async Task<bool> TryCompleteStep() {
            var args = new TryCompleteStepEventArgs();

            await OnTryCompleteStep.InvokeAsync(args);

            return !args.IsCancelled;
        }
    }
}
