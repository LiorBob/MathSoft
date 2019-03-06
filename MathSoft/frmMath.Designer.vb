<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _Partial Public Class frmMath
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _    Private Sub InitializeComponent()
        Me.MathSoftMenuStrip = New System.Windows.Forms.MenuStrip
        Me.MathCalcToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.EquationSolToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.OneUnknownToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.TwoUnknownsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.cmdSolve = New System.Windows.Forms.Button
        Me.txtResult = New System.Windows.Forms.TextBox
        Me.txtOneUnknownEquation = New System.Windows.Forms.RichTextBox
        Me.MathSoftMenuStrip.SuspendLayout()
        Me.SuspendLayout()
        '
        'MathSoftMenuStrip
        '
        Me.MathSoftMenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MathCalcToolStripMenuItem})
        Me.MathSoftMenuStrip.Location = New System.Drawing.Point(0, 0)
        Me.MathSoftMenuStrip.Name = "MathSoftMenuStrip"
        Me.MathSoftMenuStrip.Padding = New System.Windows.Forms.Padding(4, 2, 0, 2)
        Me.MathSoftMenuStrip.Size = New System.Drawing.Size(324, 24)
        Me.MathSoftMenuStrip.TabIndex = 0
        Me.MathSoftMenuStrip.Text = "MenuStrip1"
        '
        'MathCalcToolStripMenuItem
        '
        Me.MathCalcToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.EquationSolToolStripMenuItem})
        Me.MathCalcToolStripMenuItem.Name = "MathCalcToolStripMenuItem"
        Me.MathCalcToolStripMenuItem.Size = New System.Drawing.Size(114, 20)
        Me.MathCalcToolStripMenuItem.Text = "חישובים מתימטיים"
        '
        'EquationSolToolStripMenuItem
        '
        Me.EquationSolToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OneUnknownToolStripMenuItem, Me.TwoUnknownsToolStripMenuItem})
        Me.EquationSolToolStripMenuItem.Image = Global.MathSoft.My.Resources.Resources.EqualSign
        Me.EquationSolToolStripMenuItem.Name = "EquationSolToolStripMenuItem"
        Me.EquationSolToolStripMenuItem.Size = New System.Drawing.Size(150, 22)
        Me.EquationSolToolStripMenuItem.Text = "פתרון משוואות"
        '
        'OneUnknownToolStripMenuItem
        '
        Me.OneUnknownToolStripMenuItem.Image = Global.MathSoft.My.Resources.Resources.X
        Me.OneUnknownToolStripMenuItem.Name = "OneUnknownToolStripMenuItem"
        Me.OneUnknownToolStripMenuItem.Size = New System.Drawing.Size(130, 22)
        Me.OneUnknownToolStripMenuItem.Text = "נעלם אחד"
        '
        'TwoUnknownsToolStripMenuItem
        '
        Me.TwoUnknownsToolStripMenuItem.Image = Global.MathSoft.My.Resources.Resources.XY
        Me.TwoUnknownsToolStripMenuItem.Name = "TwoUnknownsToolStripMenuItem"
        Me.TwoUnknownsToolStripMenuItem.Size = New System.Drawing.Size(130, 22)
        Me.TwoUnknownsToolStripMenuItem.Text = "שני נעלמים"
        '
        'cmdSolve
        '
        Me.cmdSolve.Location = New System.Drawing.Point(129, 91)
        Me.cmdSolve.Margin = New System.Windows.Forms.Padding(2)
        Me.cmdSolve.Name = "cmdSolve"
        Me.cmdSolve.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.cmdSolve.Size = New System.Drawing.Size(75, 25)
        Me.cmdSolve.TabIndex = 2
        Me.cmdSolve.Text = "פתור!"
        Me.cmdSolve.Visible = False
        '
        'txtResult
        '
        Me.txtResult.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(177, Byte))
        Me.txtResult.ForeColor = System.Drawing.Color.DeepPink
        Me.txtResult.Location = New System.Drawing.Point(11, 132)
        Me.txtResult.Margin = New System.Windows.Forms.Padding(2)
        Me.txtResult.Name = "txtResult"
        Me.txtResult.Size = New System.Drawing.Size(302, 29)
        Me.txtResult.TabIndex = 3
        Me.txtResult.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtResult.Visible = False
        '
        'txtOneUnknownEquation
        '
        Me.txtOneUnknownEquation.Location = New System.Drawing.Point(11, 40)
        Me.txtOneUnknownEquation.Name = "txtOneUnknownEquation"
        Me.txtOneUnknownEquation.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtOneUnknownEquation.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None
        Me.txtOneUnknownEquation.Size = New System.Drawing.Size(302, 34)
        Me.txtOneUnknownEquation.TabIndex = 1
        Me.txtOneUnknownEquation.Text = ""
        Me.txtOneUnknownEquation.Visible = False
        '
        'frmMath
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(324, 211)
        Me.Controls.Add(Me.txtOneUnknownEquation)
        Me.Controls.Add(Me.txtResult)
        Me.Controls.Add(Me.cmdSolve)
        Me.Controls.Add(Me.MathSoftMenuStrip)
        Me.MainMenuStrip = Me.MathSoftMenuStrip
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.Name = "frmMath"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Text = "מתימטיקה"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.MathSoftMenuStrip.ResumeLayout(False)
        Me.MathSoftMenuStrip.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MathSoftMenuStrip As System.Windows.Forms.MenuStrip
    Friend WithEvents MathCalcToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EquationSolToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OneUnknownToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TwoUnknownsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmdSolve As System.Windows.Forms.Button
    Friend WithEvents txtResult As System.Windows.Forms.TextBox
    Friend WithEvents txtOneUnknownEquation As System.Windows.Forms.RichTextBox

End Class
