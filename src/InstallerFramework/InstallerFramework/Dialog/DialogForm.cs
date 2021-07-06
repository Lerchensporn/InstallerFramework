// <copyright file="DialogForm.cs" company="InstallerFramework Project">
// Copyright (c) 2009 Andreas Becker. License: The MIT License (MIT).
// </copyright>
// <author>Andreas Becker</author>
// <website>http://installerframework.codeplex.com</website>
// <date>15.05.2009</date>
// <summary></summary>

namespace InstallerFramework.Dialog
{
   using System;
   using System.Collections.Generic;
   using System.Windows.Forms;

   /// <summary>
   /// Stellt einen mehrstufigen Dialog als Benutzeroberfläche für Installationen dar.
   /// </summary>
   public partial class DialogForm : Form
   {
      /// <summary>
      /// Gibt den Index der aktuellen Stufe an.
      /// </summary>
      private int stepIndex = -1;

      /// <summary>
      /// Der Text, der auf dem Weiter-Button steht. Dieses Feld wird nur von Eigenschafts-Accessoren genutzt.
      /// </summary>
      private string nextButtonText;

      /// <summary>
      /// Der Text, der auf dem Zurück-Button steht. Dieses Feld wird nur von Eigenschafts-Accessoren genutzt.
      /// </summary>
      private string previousButtonText;

      /// <summary>
      /// Der Text, der auf dem Fertig-Button steht. Dieses Feld wird nur von Eigenschafts-Accessoren genutzt.
      /// </summary>
      private string finishButtonText;

      /// <summary>
      /// Der Text, der auf dem Abbrechen-Button steht. Dieses Feld wird nur von Eigenschafts-Accessoren genutzt.
      /// </summary>
      private string cancelButtonText;

      /// <summary>
      /// Die Sidebar des Dialogs.
      /// </summary>
      private UserControl sidebar;

      /// <summary>
      /// Die letzte ausgeführte Navigationsaktion des Dialogs. 
      /// </summary>
      private NavigationType lastAction;

      /// <summary>
      /// Initialisiert eine neue Instanz der <see cref="DialogForm"/>-Klasse. In diesem Konstruktor müssen die <see cref="DialogStep"/> festgelegt werden.
      /// </summary>
      public DialogForm()
      {
         this.InitializeComponent();
         this.StepControls = new List<DialogStep>();
         this.SetPropertiesDefault();
      }

      /// <summary>
      /// Initialisiert eine neue Instanz der <see cref="DialogForm"/>-Klasse.
      /// </summary>
      /// <param name="controls">Die UserControls, die die einzelnen Stufen des Dialogs repräsentieren.</param>
      public DialogForm(IList<DialogStep> controls)
         : this()
      {
         this.StepControls = controls;
      }

      /// <summary>
      /// Tritt ein, wenn auf den 'Weiter'-Button geklickt wird.
      /// </summary>
      public event EventHandler Previous;

      /// <summary>
      /// Tritt ein, wenn auf den 'Zurück'-Button geklickt wird.
      /// </summary>
      public event EventHandler Next;

      /// <summary>
      /// Tritt ein, wenn auf den 'Abbrechen'-Button geklickt wird.
      /// </summary>
      public event EventHandler Cancel;

      /// <summary>
      /// Tritt ein, wenn auf den 'Fertig'-Button geklickt wird.
      /// </summary>
      public event EventHandler Finished;

      /// <summary>
      /// Gibt an oder legt fest, welche die einzelnen Stufen des Dialogs sind.
      /// </summary>
      public IList<DialogStep> StepControls { get; private set; }

      /// <summary>
      /// Gibt qn oder legt fest, welches der Index der aktuellen Stufe ist.
      /// </summary>
      public int CurrentStepIndex
      {
         get { return this.stepIndex; }
         set { this.GotoStep(value); }
      }

      /// <summary>
      /// Gibt an, welche die aktuelle Stufe des Dialogs an.
      /// </summary>
      public DialogStep CurrentStep
      {
         get
         {
            return this.StepControls[this.stepIndex];
         }
      }

      /// <summary>
      /// Gibt an oder legt fest, ob die Sidebar angezeigt wird.
      /// </summary>
      public bool ShowSideBar { get; set; }

      /// <summary>
      /// Gibt an oder legt fest, welche die Sidebar des Dialogs ist.
      /// </summary>
      public UserControl SideBar
      {
         get
         {
            return this.sidebar;
         }

         set
         {
            this.sidebar = value;
            this.panelSidebar.Size = value.Size;

            if (!this.panelSidebar.Controls.Contains(this.SideBar))
            {
               this.panelSidebar.Controls.Add(this.SideBar);
            }
         }
      }

      /// <summary>
      /// Gibt an oder legt fest, ob navigiert wird, wenn auf den 'Weiter'-Button geklickt wurde.
      /// </summary>
      public bool AllowGotoNext { get; set; }

      /// <summary>
      /// Gibt an oder legt fest, ob navigiert wird, wenn auf den 'Zurück'-Button geklickt wurde.
      /// </summary>
      public bool AllowGotoPrevious { get; set; }

      /// <summary>
      /// Gibt an oder legt fest, ob die Anwendung nach dem Klicken auf den CancelButton geschlossen wird.
      /// </summary>
      public bool CloseOnCancel { get; set; }

      /// <summary>
      /// Gibt an oder legt fest, welcher der Standardtext des Weiter-Buttons ist.
      /// </summary>
      public string NextButtonText
      {
         get
         {
            return this.nextButtonText;
         }

         set
         {
            this.nextButtonText = value;
            if (this.stepIndex < this.StepControls.Count - 1)
            {
               this.nextButton.Text = value;
            }
         }
      }

      /// <summary>
      /// Gibt an oder legt fest, welcher der Standardtext des Zurück-Buttons ist.
      /// </summary>
      public string PreviousButtonText
      {
         get
         {
            return this.previousButtonText;
         }

         set
         {
            this.previousButtonText = value;
            this.previousButton.Text = value;
         }
      }

      /// <summary>
      /// Gibt an oder legt fest, ob die Anwengung geschlossen wird, wenn auf den Fertig-Button geklickt wird.
      /// </summary>
      public bool CloseOnFinish { get; set; }

      /// <summary>
      /// Gibt an oder legt fest, welchen Text der Fertig-Button anzeigt.
      /// </summary>
      public string FinishButtonText
      {
         get
         {
            return this.finishButtonText;
         }

         set
         {
            this.finishButtonText = value;
            if (this.stepIndex == this.StepControls.Count - 1)
            {
               this.nextButton.Text = value;
            }
         }
      }

      /// <summary>
      /// Gibt an oder legt fest, welchen Text der CancelButton anzeigt.
      /// </summary>
      public string CancelButtonText
      {
         get
         {
            return this.cancelButtonText;
         }

         set
         {
            this.cancelButtonText = value;
            this.cancelButton.Text = value;
         }
      }

      /// <summary>
      /// Stellt die Navigationsbuttons richtig ein.
      /// </summary>
      public void InitButtons()
      {
         // Die Buttons einstellen
         if (this.stepIndex == this.StepControls.Count - 1)
         {
            this.InitButtonsLastControl();
         }
         else if (this.stepIndex == 0)
         {
            this.InitButtonsFirstControl();
         }
         else
         {
            this.InitButtonsMiddleControl();
         }
      }

      /// <summary>
      /// Navigiert zu einer Stufe des Dialogs.
      /// </summary>
      /// <param name="index">Die Nummer der Stufe.</param>
      public void GotoStep(int index)
      {
         if (index < -1 || index >= this.StepControls.Count || index == this.stepIndex)
         {
            return;
         }

         FocusChangedEventArgs args;
         if (this.stepIndex != -1)
         {
            args = new FocusChangedEventArgs(this, false, NavigationType.ProgramStepChanged);
            this.CurrentStep.OnFocusChanged(args);
            if (args.CancelCurrentAction)
            {
               return;
            }

            this.panelContent.Controls.Remove(this.StepControls[this.stepIndex]);
         }

         this.stepIndex = index;
         this.InitButtons();
         if (index != -1)
         {
            args = new FocusChangedEventArgs(this, true, NavigationType.ProgramStepChanged);
            this.CurrentStep.OnFocusChanged(args);
            if (args.CancelCurrentAction)
            {
               return;
            }

            this.panelContent.Controls.Add(this.StepControls[this.stepIndex]);
         }
      }

      /// <summary>
      /// Ruft die anzugebende Stufe auf.
      /// </summary>
      /// <param name="step">Die Stufe, zu der navigiert wird.</param>
      public void GotoStep(DialogStep step)
      {
         if (step == this.StepControls[this.stepIndex])
         {
            return;
         }

         for (int i = 0; i < this.StepControls.Count; i++)
         {
            if (this.StepControls[i] == step)
            {
               this.InitButtons();
               FocusChangedEventArgs args;
               if (this.stepIndex != -1)
               {
                  args = new FocusChangedEventArgs(this, false, NavigationType.ProgramStepChanged);
                  this.CurrentStep.OnFocusChanged(args);
                  if (args.CancelCurrentAction)
                  {
                     return;
                  }

                  this.panelContent.Controls.Remove(this.StepControls[this.stepIndex]);
               }

               this.stepIndex = i;

               args = new FocusChangedEventArgs(this, true, NavigationType.ProgramStepChanged);
               this.CurrentStep.OnFocusChanged(args);
               if (args.CancelCurrentAction)
               {
                  return;
               }

               this.panelContent.Controls.Add(this.StepControls[this.stepIndex]);
            }
         }
      }

      /// <summary>
      /// Geht eine Stufe vor.
      /// </summary>
      public void GotoNext()
      {
         if (this.AllowGotoNext)
         {
            this.GotoStep(this.stepIndex + 1);
         }
      }

      /// <summary>
      /// Geht eine Stufe zurück.
      /// </summary>
      public void GotoPrevious()
      {
         if (this.AllowGotoPrevious)
         {
            this.UserActionGotoStep(this.stepIndex - 1);
         }
      }

      /// <summary>
      /// Versteckt alle Stufen, sodass im Dialog keine Stufe angezeigt wird, und setzt den Stufenindex zurück.
      /// Es ist auch möglich, GotoStep(-1) statt diese Methode aufzurufen.
      /// </summary>
      public void HideCurrentStep()
      {
         this.panelContent.Controls.Remove(this.CurrentStep);
         this.stepIndex = -1;
      }

      /// <summary>
      /// Stellt die Navigationsbuttons für die erste Stufe ein.
      /// </summary>
      public virtual void InitButtonsFirstControl()
      {
         this.previousButton.Hide();
         this.nextButton.Show();
         this.previousButton.Text = this.PreviousButtonText;
         this.nextButton.Text = this.NextButtonText;
         this.cancelButton.Text = this.CancelButtonText;
      }

      /// <summary>
      /// Stellt die Navigationsbuttons für die mitteleren Stufen ein.
      /// </summary>
      public virtual void InitButtonsMiddleControl()
      {
         this.previousButton.Show();
         this.nextButton.Show();
         this.previousButton.Text = this.PreviousButtonText;
         this.nextButton.Text = this.NextButtonText;
         this.cancelButton.Text = this.CancelButtonText;
      }

      /// <summary>
      /// Stellt die Navigationsbuttons für die letzte Stufe ein.
      /// </summary>
      public virtual void InitButtonsLastControl()
      {
         if (this.StepControls.Count > 1)
         {
            this.previousButton.Show();
         }
         else
         {
            this.previousButton.Hide();
         }

         this.nextButton.Show();
         this.previousButton.Text = this.PreviousButtonText;
         this.nextButton.Text = this.FinishButtonText;
         this.cancelButton.Text = this.CancelButtonText;
      }

      /// <summary>
      /// Ruft das <see cref="E:Previous"/>-Event auf.
      /// </summary>
      /// <param name="e">Die <see cref="System.EventArgs"/>-Instanz, die die Daten der Ereignisses enthält.</param>
      protected virtual void OnPrevious(EventArgs e)
      {
         EventHandler myevent = this.Previous;
         if (myevent != null)
         {
            myevent(this, e);
         }
      }

      /// <summary>
      /// Ruft das <see cref="E:Next"/>-Event auf.
      /// </summary>
      /// <param name="e">Die <see cref="System.EventArgs"/>-Instanz, die die Daten der Ereignisses enthält.</param>
      protected virtual void OnNext(EventArgs e)
      {
         EventHandler myevent = this.Next;
         if (myevent != null)
         {
            myevent(this, e);
         }
      }

      /// <summary>
      /// Ruft das <see cref="E:Cancel"/>-Event auf.
      /// </summary>
      /// <param name="e">Die <see cref="System.EventArgs"/>-Instanz, die die Daten der Ereignisses enthält.</param>
      protected virtual void OnCancel(EventArgs e)
      {
         EventHandler myevent = this.Cancel;
         if (myevent != null)
         {
            myevent(this, e);
         }
      }

      /// <summary>
      /// Ruft das <see cref="E:Finished"/>-Event auf.
      /// </summary>
      /// <param name="e">Die <see cref="System.EventArgs"/>-Instanz, die die Daten der Ereignisses enthält.</param>
      protected virtual void OnFinished(EventArgs e)
      {
         EventHandler myevent = this.Finished;
         if (myevent != null)
         {
            myevent(this, e);
         }
      }

      /// <summary>
      /// Legt die Standardeigenschaften des Dialogs fest.
      /// </summary>
      protected virtual void SetPropertiesDefault()
      {
         this.CloseOnFinish = true;
         this.CloseOnCancel = true;
         this.AllowGotoNext = true;
         this.AllowGotoPrevious = true;
      }

      /// <summary>
      /// Tritt ein, wenn auf den "Vor"-Button geklickt wird.
      /// </summary>
      /// <param name="sender">Die Ereignisquelle.</param>
      /// <param name="e">Die <see cref="System.EventArgs"/>-Instanz, die Ereignisdaten enthält.</param>
      private void NextButton_Click(object sender, EventArgs e)
      {
         this.OnNext(new EventArgs());
         if (this.stepIndex == this.StepControls.Count - 1)
         {
            this.OnFinished(new EventArgs());
            this.lastAction = NavigationType.Finish;
            if (this.CloseOnFinish)
            {
               this.Close();
               return;
            }
         }
         else
         {
            this.lastAction = NavigationType.Next;
         }

         if (this.AllowGotoNext)
         {
            this.UserActionGotoStep(this.stepIndex + 1);
         }
      }

      /// <summary>
      /// Tritt ein, wenn auf den "Zurück"-Button geklickt wird.
      /// </summary>
      /// <param name="sender">Die Ereignisquelle.</param>
      /// <param name="e">Die <see cref="System.EventArgs"/>-Instanz, die Ereignisdaten enthält.</param>
      private void PreviousButton_Click(object sender, EventArgs e)
      {
         this.OnPrevious(new EventArgs());
         this.lastAction = NavigationType.Previous;
         if (this.AllowGotoPrevious)
         {
            this.UserActionGotoStep(this.stepIndex - 1);
         }
      }

      /// <summary>
      /// Tritt ein, wenn auf den "Abbrechen"-Button geklickt wird.
      /// </summary>
      /// <param name="sender">Die Ereignisquelle.</param>
      /// <param name="e">Die <see cref="System.EventArgs"/>-Instanz, die Ereignisdaten enthält.</param>
      private void CancelButton_Click(object sender, EventArgs e)
      {
         this.OnCancel(new EventArgs());
         this.CurrentStep.OnFocusChanged(new FocusChangedEventArgs(this, false, NavigationType.Cancel));
         if (this.CloseOnCancel)
         {
            this.Close();
         }
      }

      /// <summary>
      /// Tritt ein, wenn auf dem navigationPanel gezeichnet wird und zeichnet einen 3DBorder.
      /// </summary>
      /// <param name="sender">Die Ereignisquelle.</param>
      /// <param name="e">Die <see cref="PaintEventArgs"/>-Instanz, die Ereignisdaten enthält.</param>
      private void NavigationPanel_Paint(object sender, PaintEventArgs e)
      {
         ControlPaint.DrawBorder3D(e.Graphics, 0, 0, this.navigationPanel.Width, this.navigationPanel.Height, Border3DStyle.Etched, Border3DSide.Top);
      }

      /// <summary>
      /// Navigiert zu einer Stufe des Dialogs.
      /// </summary>
      /// <param name="stepnumber">Die Nummer der Stufe.</param>
      private void UserActionGotoStep(int stepnumber)
      {
         if (stepnumber < 0 || stepnumber >= this.StepControls.Count || stepnumber == this.stepIndex)
         {
            return;
         }

         FocusChangedEventArgs args = new FocusChangedEventArgs(this, false, this.lastAction);
         this.CurrentStep.OnFocusChanged(args);
         if (args.CancelCurrentAction)
         {
            return;
         }

         this.panelContent.Controls.Remove(this.CurrentStep);
         this.stepIndex = stepnumber;
         this.InitButtons();

         args = new FocusChangedEventArgs(this, true, this.lastAction);
         this.CurrentStep.OnFocusChanged(args);
         if (args.CancelCurrentAction)
         {
            return;
         }

         this.panelContent.Controls.Add(this.CurrentStep);
      }
   }
}