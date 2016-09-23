<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormPrueba
	Inherits System.Windows.Forms.Form

	'Form reemplaza a Dispose para limpiar la lista de componentes.
	<System.Diagnostics.DebuggerNonUserCode()> _
	Protected Overrides Sub Dispose(ByVal disposing As Boolean)
		Try
			If disposing AndAlso components IsNot Nothing Then
				components.Dispose()
			End If
		Finally
			MyBase.Dispose(disposing)
		End Try
	End Sub

	'Requerido por el Diseñador de Windows Forms
	Private components As System.ComponentModel.IContainer

	'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
	'Se puede modificar usando el Diseñador de Windows Forms.  
	'No lo modifique con el editor de código.
	<System.Diagnostics.DebuggerStepThrough()> _
	Private Sub InitializeComponent()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.ButtonSet = New System.Windows.Forms.Button()
        Me.TrackBar1 = New System.Windows.Forms.TrackBar()
        Me.Vumetro1 = New BibliotecaMaf.Controles.Vumetro.Vumetro()
        CType(Me.TrackBar1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(117, 54)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(100, 20)
        Me.TextBox1.TabIndex = 1
        Me.TextBox1.Text = "0"
        Me.TextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'ButtonSet
        '
        Me.ButtonSet.Location = New System.Drawing.Point(127, 80)
        Me.ButtonSet.Name = "ButtonSet"
        Me.ButtonSet.Size = New System.Drawing.Size(75, 23)
        Me.ButtonSet.TabIndex = 2
        Me.ButtonSet.Text = "Set"
        Me.ButtonSet.UseVisualStyleBackColor = True
        '
        'TrackBar1
        '
        Me.TrackBar1.LargeChange = 30
        Me.TrackBar1.Location = New System.Drawing.Point(76, 205)
        Me.TrackBar1.Maximum = 100
        Me.TrackBar1.Name = "TrackBar1"
        Me.TrackBar1.Size = New System.Drawing.Size(196, 45)
        Me.TrackBar1.TabIndex = 3
        Me.TrackBar1.Value = 70
        '
        'Vumetro1
        '
        Me.Vumetro1.Alineacion = BibliotecaMaf.Controles.Vumetro.Vumetro.Alineaciones.Vertical
        Me.Vumetro1.AnchoLineaMax = 5
        Me.Vumetro1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Vumetro1.Color1 = System.Drawing.Color.Lime
        Me.Vumetro1.Color2 = System.Drawing.Color.Yellow
        Me.Vumetro1.Color3 = System.Drawing.Color.Red
        Me.Vumetro1.ColorTexto = System.Drawing.Color.Black
        Me.Vumetro1.Location = New System.Drawing.Point(12, 12)
        Me.Vumetro1.Margen = 2
        Me.Vumetro1.Maximo = 100
        Me.Vumetro1.Minimo = 0
        Me.Vumetro1.MostrarColor1 = True
        Me.Vumetro1.MostrarColor2 = True
        Me.Vumetro1.MostrarColor3 = True
        Me.Vumetro1.MostrarValor = False
        Me.Vumetro1.Name = "Vumetro1"
        Me.Vumetro1.RetardoCaidaMarcaMaximo = 50
        Me.Vumetro1.Size = New System.Drawing.Size(40, 238)
        Me.Vumetro1.TabIndex = 0
        Me.Vumetro1.TipoFuncionamiento = BibliotecaMaf.Controles.Vumetro.Vumetro.eTipoFuncionamiento.SubidaLinCaidaAuto
        Me.Vumetro1.Valor = 70
        Me.Vumetro1.VelocidadBajada = 30
        '
        'FormPrueba
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(284, 262)
        Me.Controls.Add(Me.TrackBar1)
        Me.Controls.Add(Me.ButtonSet)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.Vumetro1)
        Me.Name = "FormPrueba"
        Me.Text = "Prueba Vumetro"
        CType(Me.TrackBar1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
	Friend WithEvents Vumetro1 As BibliotecaMAF.Controles.Vumetro.Vumetro
	Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents ButtonSet As System.Windows.Forms.Button
    Friend WithEvents TrackBar1 As System.Windows.Forms.TrackBar

End Class
