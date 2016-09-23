Public Class Vumetro
    Dim mUsrValorSeteado As Integer = 0
    Dim mUsrLimiteMinimo As Integer = 0
    Dim mUsrLimiteMaximo As Integer = 100

    Dim mCtrValorSeteado As Integer = 0
    Dim mCtrLimiteMinimo As Integer = 0
    Dim mCtrLimiteMaximo As Integer = 100

    Dim mCtrValorActualEscala As Integer = 0
    Dim mCtrValorActualEscalaMax As Integer = 0

    Dim mAlineacion As Alineaciones = Alineaciones.Vertical
    Dim mTipoFuncionamiento As eTipoFuncionamiento = eTipoFuncionamiento.SubidaLinealCaidaLineal
    Dim mRectangulo As Rectangle
    Dim mRectanguloMed As Rectangle
    Dim mRectanguloMax As Rectangle
    Dim mDibujando As Boolean = False

    Dim mVelocidadBajada As Integer = 20
    Dim mRetardoCaidaMaximo As Integer = 10
    Dim mRetardoCaidaMaximoTemp As Integer = 10
    Dim mAnchoMarcaMaximo As Integer = 0

    Dim mColor1 As System.Drawing.Color = Color.Green
    Dim mColor2 As System.Drawing.Color = Color.Yellow
    Dim mColor3 As System.Drawing.Color = Color.Red
    Dim mColorTexto As System.Drawing.Color = Color.Black
    Dim BrushTexto As System.Drawing.Brush = New System.Drawing.SolidBrush(mColorTexto)
    Dim BrushVerde As System.Drawing.Brush = New System.Drawing.SolidBrush(mColor1)
    Dim BrushAmarillo As System.Drawing.Brush = New System.Drawing.SolidBrush(mColor2)
    Dim BrushMaximo As System.Drawing.Brush = New System.Drawing.SolidBrush(mColor3)
    Dim mUsarColor1 As Boolean = True
    Dim mUsarColor2 As Boolean = True
    Dim mUsarColor3 As Boolean = True

    Dim mMostrarValor As Boolean = False

    Dim SemaforoSincroVariables As New System.Threading.Semaphore(1, 1)

    Dim mMargenes As Integer = 2
    Dim WithEvents TimerBajada As New System.Timers.Timer


    Private Class Vectores
        Public Inicial As Integer
        Public Punto1 As Integer
        Public Punto2 As Integer
        Public Punto3 As Integer
        Public Punto4 As Integer
        Public Final As Integer

        Public Sub New(TamañoTotal As Integer, Margenes As Integer, AlturaMarcaMax As Integer, Proporcion1 As Double, Proporcion3 As Double)
            Inicial = 0
            Final = TamañoTotal
            Punto1 = Margenes
            Punto2 = CInt((TamañoTotal - (Margenes * 2)) * Proporcion1) + Margenes
            Punto4 = CInt((TamañoTotal - (Margenes * 2)) * Proporcion3) + Margenes
            Punto3 = Punto4 - AlturaMarcaMax
            If Punto2 < Margenes Then
                Punto2 = Margenes
            End If
            If Punto3 < Margenes Then
                Punto3 = Margenes
            End If
            If Punto4 < Margenes Then
                Punto4 = Margenes
            End If
        End Sub
    End Class

    Public Enum Alineaciones As Integer
        Vertical = 0
        Horizontal = 1
        VerticalInvertido = 2
        HorizontalInvertido = 3
    End Enum
    Public Enum eTipoFuncionamiento
        SubidaLinealCaidaLineal = 0
        SubidaLinCaidaAuto = 1
    End Enum
    Private Delegate Sub DelegadoValor(Valor As Integer)
    Private Delegate Sub DelegadoFuncion()

    Public Sub New()
        InitializeComponent()
        AddHandler TimerBajada.Elapsed, AddressOf TimerBajada_Elapsed
    End Sub

#Region "Propiedades"
    Public Property Valor As Integer
        Get
            Return mUsrValorSeteado
        End Get
        Set(value As Integer)
            If value > mUsrLimiteMaximo Then
                mUsrValorSeteado = mUsrLimiteMaximo
            ElseIf value < mUsrLimiteMinimo Then
                mUsrValorSeteado = mUsrLimiteMinimo
            Else
                mUsrValorSeteado = value
            End If

            If (TipoFuncionamiento = eTipoFuncionamiento.SubidaLinealCaidaLineal) Then
                mCtrValorActualEscala = mUsrValorSeteado
                CalcularRectangulo()
            ElseIf (TipoFuncionamiento = eTipoFuncionamiento.SubidaLinCaidaAuto) Then
                If (mCtrValorActualEscala < mUsrValorSeteado) Then
                    mCtrValorActualEscala = mUsrValorSeteado
                    CalcularRectangulo()
                End If
            End If

            If mCtrValorActualEscalaMax <= mUsrValorSeteado Then
                mCtrValorActualEscalaMax = mUsrValorSeteado
                mRetardoCaidaMaximoTemp = mRetardoCaidaMaximo
            End If

            Redibujar()
            ActivarTimer()
        End Set
    End Property
    Public Property Maximo As Integer
        Get
            Return mUsrLimiteMaximo
        End Get
        Set(value As Integer)
            mUsrLimiteMaximo = value
            If value < mUsrValorSeteado Then
                mUsrValorSeteado = value
            End If
            CalcularRectangulo()
            Redibujar()
        End Set
    End Property
    Public Property Minimo As Integer
        Get
            Return mUsrLimiteMinimo
        End Get
        Set(value As Integer)
            mUsrLimiteMinimo = value
            If value > mUsrValorSeteado Then
                mUsrValorSeteado = value
            End If
            CalcularRectangulo()
            Redibujar()
        End Set
    End Property
    Public Property AnchoLineaMax As Integer
        Get
            Return mAnchoMarcaMaximo
        End Get
        Set(value As Integer)
            mAnchoMarcaMaximo = value
            CalcularRectangulo()
            Redibujar()
        End Set
    End Property
    Public Property RetardoCaidaMarcaMaximo As Integer
        Get
            Return mRetardoCaidaMaximo
        End Get
        Set(value As Integer)
            mRetardoCaidaMaximo = value
            mRetardoCaidaMaximoTemp = value
        End Set
    End Property
    Public Property Alineacion As Alineaciones
        Get
            Return mAlineacion
        End Get
        Set(value As Alineaciones)
            mAlineacion = value
            CalcularRectangulo()
            Redibujar()
        End Set
    End Property
    Public Property TipoFuncionamiento As eTipoFuncionamiento
        Get
            Return mTipoFuncionamiento
        End Get
        Set(value As eTipoFuncionamiento)
            mTipoFuncionamiento = value
            CalcularRectangulo()
            Redibujar()
        End Set
    End Property
    Public Property Color1 As Color
        Get
            Return mColor1
        End Get
        Set(value As Color)
            mColor1 = value
            BrushVerde = New System.Drawing.SolidBrush(mColor1)
            Redibujar()
        End Set
    End Property
    Public Property Color2 As Color
        Get
            Return mColor2
        End Get
        Set(value As Color)
            mColor2 = value
            BrushAmarillo = New System.Drawing.SolidBrush(mColor2)
            Redibujar()
        End Set
    End Property
    Public Property Color3 As Color
        Get
            Return mColor3
        End Get
        Set(value As Color)
            mColor3 = value
            BrushMaximo = New System.Drawing.SolidBrush(mColor3)
            Redibujar()
        End Set
    End Property
    Public Property ColorTexto As Color
        Get
            Return mColorTexto
        End Get
        Set(value As Color)
            mColorTexto = value
            BrushTexto = New System.Drawing.SolidBrush(mColorTexto)
            Redibujar()
        End Set
    End Property
    Public Property Margen As Integer
        Get
            Return mMargenes
        End Get
        Set(value As Integer)
            mMargenes = value
            CalcularRectangulo()
            Redibujar()
        End Set
    End Property
    Public Property VelocidadBajada As Integer
        Get
            Return mVelocidadBajada
        End Get
        Set(value As Integer)
            mVelocidadBajada = value
        End Set
    End Property
    Public Property MostrarColor1 As Boolean
        Get
            Return mUsarColor1
        End Get
        Set(value As Boolean)
            mUsarColor1 = value
            Redibujar()
        End Set
    End Property
    Public Property MostrarColor2 As Boolean
        Get
            Return mUsarColor2
        End Get
        Set(value As Boolean)
            mUsarColor2 = value
            Redibujar()
        End Set
    End Property
    Public Property MostrarColor3 As Boolean
        Get
            Return mUsarColor3
        End Get
        Set(value As Boolean)
            mUsarColor3 = value
            Redibujar()
        End Set
    End Property
    Public Property MostrarValor As Boolean
        Get
            Return mMostrarValor
        End Get
        Set(value As Boolean)
            mMostrarValor = value
            Redibujar()
        End Set
    End Property
#End Region

#Region "ControlEventos"
    Protected Overrides Sub OnPaint(e As System.Windows.Forms.PaintEventArgs)
        'MyBase.OnPaint(e)
        Dim G = e.Graphics
        SemaforoSincroVariables.WaitOne()
        If mUsarColor1 = True Then
            G.FillRectangle(BrushVerde, mRectangulo)
        End If
        If mUsarColor2 = True Then
            G.FillRectangle(BrushAmarillo, mRectanguloMed)
        End If
        If mUsarColor3 = True Then
            G.FillRectangle(BrushMaximo, mRectanguloMax)
        End If
        SemaforoSincroVariables.Release()
        If mMostrarValor = True Then
            Dim Tamaño As System.Drawing.SizeF = G.MeasureString(mUsrValorSeteado.ToString, Me.Font)
            G.DrawString(mUsrValorSeteado.ToString, Me.Font, BrushTexto, CSng(((Me.Width - Tamaño.Width) / 2) - 3), CSng(((Me.Height - Tamaño.Height) / 2) - 3))
        End If
        mDibujando = False
    End Sub
    Private Sub TimerBajada_Elapsed(sender As System.Object, e As System.Timers.ElapsedEventArgs)
        If (TipoFuncionamiento = eTipoFuncionamiento.SubidaLinCaidaAuto) Then
            mCtrValorActualEscala = CInt(mCtrValorActualEscala - (mUsrLimiteMaximo * 0.05))
            If (mCtrValorActualEscala < mUsrValorSeteado) Then
                mCtrValorActualEscala = mUsrValorSeteado
            End If
        End If

        If (mRetardoCaidaMaximoTemp = 0) Then
            mCtrValorActualEscalaMax = CInt(mCtrValorActualEscalaMax - (mUsrLimiteMaximo * 0.05))
            If (mCtrValorActualEscalaMax < mUsrValorSeteado) Then
                mCtrValorActualEscalaMax = mUsrValorSeteado
                mRetardoCaidaMaximoTemp = RetardoCaidaMarcaMaximo
            Else
                ActivarTimer()
            End If
        Else
            mRetardoCaidaMaximoTemp = mRetardoCaidaMaximoTemp - 1
            ActivarTimer()
        End If

        CalcularRectangulo()
        Redibujar()
    End Sub
    Private Sub Vumetro_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        CalcularRectangulo()
        If (mDibujando = False) Then
            mDibujando = True
            Me.Invalidate()
        End If
    End Sub
#End Region

    Private Sub Redibujar()
        'SyncLock SemaforoSincro
        If (mDibujando = False) Then
            mDibujando = True
            RedibujarA()
        End If
        'End SyncLock
    End Sub
    Private Sub RedibujarA()
        If (Me.InvokeRequired = True) Then
            Dim D As New DelegadoFuncion(AddressOf RedibujarA)
            Me.Invoke(D)
        Else
            Me.Invalidate()
        End If
    End Sub

#Region "Procedimientos"
    ''' <summary>
    ''' Para setear el valor desde cualquier subProceso
    ''' </summary>
    ''' <param name="Valor"></param>
    ''' <remarks></remarks>
    Public Sub SetValor(Valor As Integer)
        If Me.InvokeRequired = True Then
            Dim D As New DelegadoValor(AddressOf SetValor)
            Me.Invoke(D, Valor)
        Else
            Me.Valor = Valor
        End If
    End Sub
    ''' <summary>
    ''' Para setear el valor desde cualquier subProceso de forma asincrona
    ''' </summary>
    ''' <param name="Valor"></param>
    ''' <remarks></remarks>
    Public Sub SetValorAsync(Valor As Integer)
        If Me.InvokeRequired = True Then
            Dim D As New DelegadoValor(AddressOf SetValor)
            Me.BeginInvoke(D, Valor)
        Else
            Me.Valor = Valor
        End If
    End Sub
    Private Sub CalcularRectangulo()

        SemaforoSincroVariables.WaitOne()
        Select Case Alineacion
            Case Alineaciones.VerticalInvertido
                Dim V As New Vectores(ClientRectangle.Height, mMargenes, mAnchoMarcaMaximo, mCtrValorActualEscala / (mUsrLimiteMaximo - mUsrLimiteMinimo), mCtrValorActualEscalaMax / (mUsrLimiteMaximo - mUsrLimiteMinimo))
                mRectangulo.X = mMargenes
                mRectangulo.Width = ClientRectangle.Width - (mMargenes * 2)
                mRectangulo.Y = V.Punto1
                mRectangulo.Height = V.Punto2 - V.Punto1

                mRectanguloMed.X = mMargenes
                mRectanguloMed.Width = mRectangulo.Width
                mRectanguloMed.Y = V.Punto2
                mRectanguloMed.Height = V.Punto3 - V.Punto2

                mRectanguloMax.X = mMargenes
                mRectanguloMax.Width = mRectangulo.Width
                mRectanguloMax.Y = V.Punto3
                mRectanguloMax.Height = V.Punto4 - V.Punto3
            Case Alineaciones.Vertical
                Dim V As New Vectores(ClientRectangle.Height, mMargenes, mAnchoMarcaMaximo, mCtrValorActualEscala / (mUsrLimiteMaximo - mUsrLimiteMinimo), mCtrValorActualEscalaMax / (mUsrLimiteMaximo - mUsrLimiteMinimo))
                mRectangulo.X = mMargenes
                mRectangulo.Width = ClientRectangle.Width - (mMargenes * 2)
                mRectangulo.Y = (ClientRectangle.Height) - V.Punto2
                mRectangulo.Height = V.Punto2 - V.Punto1

                mRectanguloMed.X = mMargenes
                mRectanguloMed.Width = mRectangulo.Width
                mRectanguloMed.Y = (ClientRectangle.Height) - V.Punto3
                mRectanguloMed.Height = V.Punto3 - V.Punto2

                mRectanguloMax.X = mMargenes
                mRectanguloMax.Width = mRectangulo.Width
                mRectanguloMax.Y = (ClientRectangle.Height) - V.Punto4
                mRectanguloMax.Height = V.Punto4 - V.Punto3
            Case Alineaciones.Horizontal
                Dim V As New Vectores(ClientRectangle.Width, mMargenes, mAnchoMarcaMaximo, mCtrValorActualEscala / (mUsrLimiteMaximo - mUsrLimiteMinimo), mCtrValorActualEscalaMax / (mUsrLimiteMaximo - mUsrLimiteMinimo))
                mRectangulo.Y = mMargenes
                mRectangulo.Height = ClientRectangle.Height - (mMargenes * 2)
                mRectangulo.X = V.Punto1
                mRectangulo.Width = V.Punto2 - V.Punto1

                mRectanguloMed.Y = mMargenes
                mRectanguloMed.Height = mRectangulo.Height
                mRectanguloMed.X = V.Punto2
                mRectanguloMed.Width = V.Punto3 - V.Punto2

                mRectanguloMax.Y = mMargenes
                mRectanguloMax.Height = mRectangulo.Height
                mRectanguloMax.X = V.Punto3
                mRectanguloMax.Width = V.Punto4 - V.Punto3
            Case Alineaciones.HorizontalInvertido
                Dim V As New Vectores(ClientRectangle.Width, mMargenes, mAnchoMarcaMaximo, mCtrValorActualEscala / (mUsrLimiteMaximo - mUsrLimiteMinimo), mCtrValorActualEscalaMax / (mUsrLimiteMaximo - mUsrLimiteMinimo))
                mRectangulo.Y = mMargenes
                mRectangulo.Height = ClientRectangle.Height - (mMargenes * 2)
                mRectangulo.X = (ClientRectangle.Width) - V.Punto2
                mRectangulo.Width = V.Punto2 - V.Punto1

                mRectanguloMed.Y = mMargenes
                mRectanguloMed.Height = mRectangulo.Height
                mRectanguloMed.X = (ClientRectangle.Width) - V.Punto3
                mRectanguloMed.Width = V.Punto3 - V.Punto2

                mRectanguloMax.Y = mMargenes
                mRectanguloMax.Height = mRectangulo.Height
                mRectanguloMax.X = (ClientRectangle.Width) - V.Punto4
                mRectanguloMax.Width = V.Punto4 - V.Punto3
        End Select
        SemaforoSincroVariables.Release()
    End Sub
    Private Sub ActivarTimer()
        If TimerBajada.Enabled = False Then
            TimerBajada.AutoReset = False
            TimerBajada.Interval = mVelocidadBajada
            TimerBajada.Start()
        End If
    End Sub
    Private Sub DetenerTimer()
        TimerBajada.Stop()
    End Sub
#End Region

End Class
