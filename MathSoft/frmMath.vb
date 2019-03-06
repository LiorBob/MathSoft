Public Class frmMath

    Private strNewSideValue As String
    Private intSelectionStart As Integer = 0
    Private blnDecimalPointFound As Boolean = False
    Private objInputTextOriginalSize As New Size
    
    Const APPLICATION_NAME = "מתימטיקה"
    Const RESOURCES_PATH = "..\..\resources\"
    Const NO_SOLUTION = "אין פתרון למשוואה."
    Const EVERY_REAL = "פתרון המשוואה הוא כל X ממשי."
    Const ERROR_NO_EQUAL_SIGN = "= במשוואה חייב להופיע הסימן"
    Const ERROR_NO_X = "X או x במשוואה חייב להופיע"
    Const ERROR_SIGN_NOT_RIGHTMOST = "הסימנים . / * - + = לא יכולים להופיע בקצה הימני של המשוואה"
    Const ERROR_OVERFLOW = "במשוואה , או בפתרונה , מופיע ערך החורג מטווח הערכים המותר"

    Private Sub frmMath_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        frmSplash.Hide()

    End Sub

    Private Sub frmMath_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

        frmSplash.Close()

    End Sub

    Private Sub OneUnknownToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OneUnknownToolStripMenuItem.Click

        objInputTextOriginalSize = txtOneUnknownEquation.Size

        txtOneUnknownEquation.Visible = False
        cmdSolve.Visible = False

        My.Computer.Audio.Play(My.Resources.EnterEquation, AudioPlayMode.WaitToComplete)

        txtOneUnknownEquation.Visible = True
        cmdSolve.Visible = True

        txtOneUnknownEquation.Clear()

        'Puts the focus on the equation text box ,
        'so the user can type the equation right
        'after the spoken announcement .

        txtOneUnknownEquation.Focus()

    End Sub

    Private Sub cmdSolve_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSolve.Click

        txtResult.Clear()

        Me.Cursor = Cursors.WaitCursor

        If InStr(txtOneUnknownEquation.Text, "=") = 0 Then

            'Equal sign (=) not found
            MsgBox(ERROR_NO_EQUAL_SIGN, MsgBoxStyle.MsgBoxRight, APPLICATION_NAME)
            Me.Cursor = Cursors.Default

            Exit Sub

        End If

        If InStr(txtOneUnknownEquation.Text, "x") = 0 And InStr(txtOneUnknownEquation.Text, "X") = 0 Then

            'nor x neither X was found
            MsgBox(ERROR_NO_X, MsgBoxStyle.MsgBoxRight, APPLICATION_NAME)
            Me.Cursor = Cursors.Default

            Exit Sub

        End If



        'Right side is empty  (ie. one of the signs
        '= + - * / .  is the rightmost character in the original 
        'equation) : report an error .

        If txtOneUnknownEquation.Text.LastIndexOf("=") = txtOneUnknownEquation.Text.Length - 1 Or txtOneUnknownEquation.Text.LastIndexOf("+") = txtOneUnknownEquation.Text.Length - 1 Or txtOneUnknownEquation.Text.LastIndexOf("-") = txtOneUnknownEquation.Text.Length - 1 Or txtOneUnknownEquation.Text.LastIndexOf("*") = txtOneUnknownEquation.Text.Length - 1 Or txtOneUnknownEquation.Text.LastIndexOf("/") = txtOneUnknownEquation.Text.Length - 1 Or txtOneUnknownEquation.Text.LastIndexOf(".") = txtOneUnknownEquation.Text.Length - 1 Then

            MsgBox(ERROR_SIGN_NOT_RIGHTMOST, MsgBoxStyle.MsgBoxRight, APPLICATION_NAME)
            Me.Cursor = Cursors.Default

            Exit Sub

        End If


        Call SolveEquation()

    End Sub

    Private Sub SolveEquation()

        Dim strOriginalEquation As String
        Dim strLeftSide, strNewLeftSide As String
        Dim strRightSide, strNewRightSide As String
        Dim strEquationBeforeResult As String
        Dim strFinalResult As String
        Dim intEqualSignPosition As Integer


        strOriginalEquation = txtOneUnknownEquation.Text
        intEqualSignPosition = strOriginalEquation.IndexOf("=")

        strLeftSide = Strings.Left(strOriginalEquation, intEqualSignPosition)
        strRightSide = strOriginalEquation.Substring(intEqualSignPosition + 1)

        strNewLeftSide = InsertOnesPlus(strLeftSide)
        strNewRightSide = InsertOnesPlus(strRightSide)

        strNewLeftSide = CollectTerms(strNewLeftSide)
        strNewRightSide = CollectTerms(strNewRightSide)


        If strNewLeftSide = ERROR_OVERFLOW OrElse strNewRightSide = ERROR_OVERFLOW Then

            MsgBox(ERROR_OVERFLOW, MsgBoxStyle.MsgBoxRight, APPLICATION_NAME)
            Me.Cursor = Cursors.Default

            Exit Sub

        End If


        strEquationBeforeResult = CollectAllTerms(strNewLeftSide & "=" & strNewRightSide)
        strFinalResult = CalculateResult(strEquationBeforeResult)


        'If strFinalResult contains Infinity , it means that the
        'calculation in CalculateResult exceeded the range of Double .

        If strFinalResult.Contains("Infinity") Then

            MsgBox(ERROR_OVERFLOW, MsgBoxStyle.MsgBoxRight, APPLICATION_NAME)
            Me.Cursor = Cursors.Default

            Exit Sub

        End If


        Call ShowResult(strFinalResult)

        txtResult.Visible = False

        intSelectionStart = 0

        txtOneUnknownEquation.Clear()
        txtOneUnknownEquation.Focus()

    End Sub

    Private Function InsertOnesPlus(ByVal strSide As String) As String

        Dim strNewSide As String = strSide
        Dim strCurrent, strNext As String

        Dim intCurrentPosition As Integer


        'puts "1" in the beginning of the side ,
        'if x is the first character in that side.

        If LCase(GetChar(strNewSide, 1)) = "x" Then

            strNewSide = "1" & strNewSide

        End If


        'if the combination "-x" or "+x" is found ,
        'then it is converted to "-1x" or "+1x" ,
        'respectively .

        For intCurrentPosition = strNewSide.Length - 1 To 1 Step -1

            strCurrent = GetChar(strNewSide, intCurrentPosition)
            strNext = GetChar(strNewSide, intCurrentPosition + 1)

            If (strCurrent = "-" Or strCurrent = "+") And LCase(strNext) = "x" Then

                strNewSide = strNewSide.Insert(intCurrentPosition, "1")

            End If

        Next


        'If the side begins with a digit , then
        'the side will start with "+" .

        If IsNumeric(GetChar(strNewSide, 1)) Then

            strNewSide = "+" & strNewSide

        End If

        Return strNewSide

    End Function

    Private Function CollectTerms(ByVal strSide As String) As String

        Dim strNewSide As String = strSide
        Dim strTotalX As String
        Dim strTotalFree As String


        strTotalX = CollectXTerms(strNewSide)

        If strTotalX = ERROR_OVERFLOW Then

            Return ERROR_OVERFLOW

        End If


        strNewSide = AssignStrNewSide

        strTotalFree = CollectFreeTerms(strNewSide)

        If strTotalFree = ERROR_OVERFLOW Then

            Return ERROR_OVERFLOW

        End If


        If GetChar(strTotalFree, 1) <> "-" Then

            strNewSide = strTotalX & "+" & strTotalFree

        Else

            strNewSide = strTotalX & strTotalFree

        End If

        Return strNewSide

    End Function

    Private Function CollectXTerms(ByVal strSide As String) As String

        Dim strNewSide As String = strSide
        Dim strXCoefficient As String
        Dim strTotalX As String
        Dim strCurrent As String

        Dim dblSumXCoefficients As Double = 0

        Dim intCurrentXPosition As Integer = 0
        Dim intCurrentPosition As Integer


        'Scans the side from right to left , in order
        'to find the X coefficients . 
        'dblSumXCoefficients holds the sum of all of
        'the X coefficients in the same side .

        For intCurrentPosition = strNewSide.Length To 1 Step -1

            strCurrent = GetChar(strNewSide, intCurrentPosition)

            If LCase(strCurrent) = "x" Then

                intCurrentXPosition = intCurrentPosition

            End If

            If (strCurrent = "-" Or strCurrent = "+") And intCurrentXPosition <> 0 Then

                strXCoefficient = Mid(strNewSide, intCurrentPosition, intCurrentXPosition - intCurrentPosition)


                Try

                    dblSumXCoefficients += Val(strXCoefficient)

                Catch e As OverflowException

                    Return ERROR_OVERFLOW

                End Try


                'The following line helps collecting the free coefficients .
                'It removes all the instances of X coefficients in the same side .
                'The start index for removing is intCurrentPosition - 1 , because
                'the Remove method works with 0-based arrays .

                strNewSide = strNewSide.Remove(intCurrentPosition - 1, intCurrentXPosition - intCurrentPosition + 1)

                intCurrentXPosition = 0

            End If

        Next


        'Formatting dblSumXCoefficients to strTotalX .

        strTotalX = String.Format("{0:0.###################}", dblSumXCoefficients) & "x"


        AssignStrNewSide = strNewSide

        Return strTotalX

    End Function

    Private Function CollectFreeTerms(ByVal strSide As String) As String

        Dim aStrFreeCoefficients() As String

        Dim strNewSide As String = strSide
        Dim strTotalFree As String
        Dim strCurrent As String

        Dim dblSumFreeCoefficients As Double = 0

        Dim intCurrentPosition As Integer
        Dim intElementIndex As Integer


        'Separates the free coefficients using the
        'space character .

        For intCurrentPosition = strNewSide.Length To 1 Step -1

            strCurrent = GetChar(strNewSide, intCurrentPosition)

            If strCurrent = "+" Or strCurrent = "-" Then

                strNewSide = strNewSide.Insert(intCurrentPosition - 1, " ")

            End If

        Next


        'Builds a string array with the free coefficients .

        aStrFreeCoefficients = Split(strNewSide)


        'dblSumFreeCoefficients holds the sum of all of
        'the free coefficients in the same side .

        For intElementIndex = 1 To UBound(aStrFreeCoefficients)

            Try

                dblSumFreeCoefficients += Val(aStrFreeCoefficients(intElementIndex))

            Catch exception As OverflowException

                Return ERROR_OVERFLOW

            End Try

        Next



        'Formatting dblSumFreeCoefficients to strTotalFree .

        strTotalFree = String.Format("{0:0.###################}", dblSumFreeCoefficients)


        Return strTotalFree

    End Function

    'Collects terms (X-terms and free terms ,
    'separately) from both sides of the equation .

    Private Function CollectAllTerms(ByVal strEquation As String) As String

        Dim strTotalX As String
        Dim strTotalFree As String

        Dim strCurrent As String

        Dim intEqualSignPosition As Integer
        Dim intCurrentPosition As Integer

        intEqualSignPosition = InStr(strEquation, "=")


        'Checks if the first character after equal sign
        'is a digit. If yes , its sign is needed to be "-" .
        'In the Else part : Character after equal sign
        'is "-" : needed to be replaced with "+" .
        'This is sign replacement in the right side .

        If IsNumeric(GetChar(strEquation, intEqualSignPosition + 1)) Then

            strEquation = strEquation.Insert(intEqualSignPosition, "-")

        Else

            Mid(strEquation, intEqualSignPosition + 1, 1) = "+"

        End If


        intCurrentPosition = intEqualSignPosition - 1


        'The following loop locates the position of 
        'the sign to replace ("+" or "-") in the left side .

        Do

            intCurrentPosition -= 1
            strCurrent = GetChar(strEquation, intCurrentPosition)

        Loop Until strCurrent = "+" Or strCurrent = "-"


        'The following code is sign replacement in the left side .

        If strCurrent = "+" Then

            Mid(strEquation, intCurrentPosition, 1) = "-"

        Else

            Mid(strEquation, intCurrentPosition, 1) = "+"

        End If


        'Removes the "=" sign from the equation ,
        'in order to work properly with the
        'terms-collecting functions.

        strEquation = strEquation.Remove(intEqualSignPosition - 1, 1)


        'The following If statement is similar to 
        'the "Plus" part in InsertOnesPlus function.

        If IsNumeric(GetChar(strEquation, 1)) Then

            strEquation = "+" & strEquation

        End If


        strTotalX = CollectXTerms(strEquation)
        strEquation = AssignStrNewSide
        strTotalFree = CollectFreeTerms(strEquation)

        Return strTotalX & "=" & strTotalFree

    End Function

    Private Function CalculateResult(ByVal strEquation As String) As String

        Dim strFreeCoefficient As String
        Dim strXCoefficient As String

        Dim strResult As String

        Dim dblFreeCoefficient As Double
        Dim dblXCoefficient As Double

        Dim dblResult As Double

        Dim intEqualSignPosition As Integer
        Dim intXPosition As Integer


        intEqualSignPosition = strEquation.IndexOf("=")
        intXPosition = strEquation.IndexOf("x")

        strFreeCoefficient = strEquation.Substring(intEqualSignPosition + 1)
        strXCoefficient = Strings.Left(strEquation, intXPosition)

        dblFreeCoefficient = Val(strFreeCoefficient)
        dblXCoefficient = Val(strXCoefficient)


        If dblXCoefficient = 0.0 Then

            If dblFreeCoefficient <> 0.0 Then

                Return NO_SOLUTION

            Else

                Return EVERY_REAL

            End If

        End If


        dblResult = dblFreeCoefficient / dblXCoefficient


        'Formatting dblResult to strResult .

        strResult = "x=" & String.Format("{0:0.###################}", dblResult)


        Return strResult

    End Function

    Private Sub ShowResult(ByVal strResult As String)

        txtResult.Visible = False

        Select Case strResult

            Case NO_SOLUTION

                txtResult.TextAlign = HorizontalAlignment.Left
                txtResult.Text = NO_SOLUTION

                My.Computer.Audio.Play(RESOURCES_PATH + "nosolution.wav", AudioPlayMode.WaitToComplete)
                TranslateEquation(txtOneUnknownEquation.Text)

                txtResult.Visible = True
                txtResult.Refresh()

                'Sleeps for 2 seconds , to see no solution

                System.Threading.Thread.Sleep(2000)

            Case EVERY_REAL

                txtResult.TextAlign = HorizontalAlignment.Left
                txtResult.Text = EVERY_REAL

                My.Computer.Audio.Play(RESOURCES_PATH + "solution.wav", AudioPlayMode.WaitToComplete)
                TranslateEquation(txtOneUnknownEquation.Text)

                txtResult.Visible = True
                txtResult.Refresh()

                My.Computer.Audio.Play(RESOURCES_PATH + "everyreal.wav", AudioPlayMode.WaitToComplete)

            Case Else

                txtResult.TextAlign = HorizontalAlignment.Right
                txtResult.Text = strResult

                My.Computer.Audio.Play(RESOURCES_PATH + "solution.wav", AudioPlayMode.WaitToComplete)
                TranslateEquation(txtOneUnknownEquation.Text)

                My.Computer.Audio.Play(RESOURCES_PATH + "is.wav", AudioPlayMode.WaitToComplete)
                txtResult.Visible = True
                txtResult.Refresh()

                TranslateEquation(strResult)

        End Select

        txtOneUnknownEquation.Size = objInputTextOriginalSize
        txtOneUnknownEquation.Refresh()

        cmdSolve.Location = New Point(cmdSolve.Location.X, txtOneUnknownEquation.Location.Y + txtOneUnknownEquation.Height + 17)
        cmdSolve.Refresh()

        Me.Cursor = Cursors.Default

    End Sub

    'The following property is for retrieving strNewSide
    'after collecting X terms (strNewSide no longer
    'contains the X terms - it contains only the free coefficients) .

    Private Property AssignStrNewSide()
        Get
            Return strNewSideValue
        End Get
        Set(ByVal value)
            strNewSideValue = value
        End Set
    End Property

    'Looks at all parts of the equation,in order to
    'say it correctly .

    Private Sub TranslateEquation(ByVal strEquation As String)

        Dim strToSpeak As String = ""
        Dim strCurrent As String

        Dim intCurrentPosition As Integer = 1


        Do

            strCurrent = GetChar(strEquation, intCurrentPosition)

            If IsNumeric(strCurrent) Then

                strToSpeak += strCurrent

                If intCurrentPosition = Len(strEquation) Then

                    PaintText(Len(strToSpeak))
                    SayTheNumber(strToSpeak)
                    strToSpeak = ""

                End If

            Else

                If strToSpeak <> "" Then

                    PaintText(Len(strToSpeak))
                    SayTheNumber(strToSpeak)
                    strToSpeak = ""

                End If

                If strCurrent = "+" Or strCurrent = "=" Then

                    PaintText(1)
                    My.Computer.Audio.Play(RESOURCES_PATH + strCurrent + ".wav", AudioPlayMode.WaitToComplete)

                ElseIf strCurrent = "-" Then

                    PaintText(1)

                    If (intCurrentPosition = 1 OrElse GetChar(strEquation, intCurrentPosition - 1) = "=") Then

                        My.Computer.Audio.Play(RESOURCES_PATH + "minus.wav", AudioPlayMode.WaitToComplete)

                    Else

                        My.Computer.Audio.Play(RESOURCES_PATH + strCurrent + ".wav", AudioPlayMode.WaitToComplete)

                    End If

                ElseIf LCase(strCurrent) = "x" Then

                    PaintText(1)
                    My.Computer.Audio.Play(RESOURCES_PATH + LCase(strCurrent) + ".wav", AudioPlayMode.WaitToComplete)

                ElseIf strCurrent = "*" Then

                    PaintText(1)
                    My.Computer.Audio.Play(RESOURCES_PATH + "mul.wav", AudioPlayMode.WaitToComplete)

                ElseIf strCurrent = "/" Then

                    PaintText(1)
                    My.Computer.Audio.Play(RESOURCES_PATH + "div.wav", AudioPlayMode.WaitToComplete)

                ElseIf strCurrent = "." Then

                    PaintText(1)
                    My.Computer.Audio.Play(RESOURCES_PATH + "point.wav", AudioPlayMode.WaitToComplete)
                    blnDecimalPointFound = True

                End If

            End If

            intCurrentPosition += 1

        Loop Until intCurrentPosition > Len(strEquation)

    End Sub

    'Separates the number into digits , then says it.

    Private Sub SayTheNumber(ByVal strToSpeak As String)

        Dim intCounter As Integer
        Dim intDigitPosition As Integer
        Dim strDigit As String
        Dim strDigitToSpeak As String


        If blnDecimalPointFound OrElse Len(strToSpeak) > 4 Then

            For intCounter = 1 To Len(strToSpeak)

                strDigit = GetChar(strToSpeak, intCounter)
                My.Computer.Audio.Play(RESOURCES_PATH + strDigit + ".wav", AudioPlayMode.WaitToComplete)

            Next

            blnDecimalPointFound = False

            Exit Sub

        End If


        If strToSpeak <= 20 Then

            My.Computer.Audio.Play(RESOURCES_PATH + strToSpeak + ".wav", AudioPlayMode.WaitToComplete)
            Exit Sub

        End If


        For intCounter = 1 To Len(strToSpeak)

            strDigit = GetChar(strToSpeak, intCounter)
            intDigitPosition = Len(strToSpeak) - intCounter

            If strDigit = 1 AndAlso intDigitPosition = 1 Then

                Dim strTwoRightmostDigits As String

                strTwoRightmostDigits = Strings.Right(strToSpeak, 2)

                Select Case strTwoRightmostDigits

                    Case 12, 13, 17, 18, 19

                        My.Computer.Audio.Play(RESOURCES_PATH + "u.wav", AudioPlayMode.WaitToComplete)

                    Case Else

                        My.Computer.Audio.Play(RESOURCES_PATH + "and.wav", AudioPlayMode.WaitToComplete)

                End Select

                My.Computer.Audio.Play(RESOURCES_PATH + strTwoRightmostDigits + ".wav", AudioPlayMode.WaitToComplete)

                Exit For

            End If

            strDigitToSpeak = strDigit * (10 ^ intDigitPosition)

            If strDigit <> 0 Then

                If intCounter = Len(strToSpeak) Then

                    If strDigit <> 2 And strDigit <> 8 Then

                        My.Computer.Audio.Play(RESOURCES_PATH + "and.wav", AudioPlayMode.WaitToComplete)

                    Else

                        My.Computer.Audio.Play(RESOURCES_PATH + "u.wav", AudioPlayMode.WaitToComplete)

                    End If

                End If

                My.Computer.Audio.Play(RESOURCES_PATH + strDigitToSpeak + ".wav", AudioPlayMode.WaitToComplete)

            End If

        Next

    End Sub

    Private Sub PaintText(ByVal intSelectionLength As Integer)

        'txtResult.Visible = True  after reading the 
        'original equation , right before reading the
        'solution :  PaintText is ignored .

        If txtResult.Visible Then

            Exit Sub

        End If

        txtOneUnknownEquation.SelectionStart = intSelectionStart
        txtOneUnknownEquation.SelectionLength = intSelectionLength
        txtOneUnknownEquation.SelectionColor = Color.DeepSkyBlue
        txtOneUnknownEquation.SelectionFont = New Font("Times New Roman", objInputTextOriginalSize.Height / 2, FontStyle.Bold + FontStyle.Italic, GraphicsUnit.Point)



        'intCurrentWidth keeps track of the current height
        'of the input textbox , in order to expand it
        'whenever needed .

        Static intCurrentWidth As Integer



        'The following If-Else section adjusts the width
        'and height of the input textbox , while reading
        'the original equation .

        If txtOneUnknownEquation.Width + (objInputTextOriginalSize.Height / 2) < Me.Width - (objInputTextOriginalSize.Height / 2) Then

            txtOneUnknownEquation.Width += objInputTextOriginalSize.Height / 2
            intCurrentWidth = txtOneUnknownEquation.Width

        Else

            If intCurrentWidth + (objInputTextOriginalSize.Height / 2) >= Me.Width - (objInputTextOriginalSize.Height / 2) Then

                txtOneUnknownEquation.Height += objInputTextOriginalSize.Height
                intCurrentWidth = objInputTextOriginalSize.Width - (objInputTextOriginalSize.Height / 2)

            End If

            intCurrentWidth += objInputTextOriginalSize.Height / 2

        End If



        'The following refresh actually performs the 
        'width/height adjustment above mentioned ,
        'and the next four statements determine the 
        'new locations of the solve button and the result
        'output textbox .

        txtOneUnknownEquation.Refresh()

        cmdSolve.Location = New Point(cmdSolve.Location.X, txtOneUnknownEquation.Location.Y + txtOneUnknownEquation.Height + 17)
        cmdSolve.Refresh()

        txtResult.Location = New Point(txtResult.Location.X, txtOneUnknownEquation.Location.Y + txtOneUnknownEquation.Height + 58)
        txtResult.Refresh()

        intSelectionStart += intSelectionLength

    End Sub

    Private Sub txtOneUnknownEquation_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtOneUnknownEquation.KeyPress

        'The following If statement does not allow the equal sign "="
        'to be the leftmost character in the input , and does not allow
        'the equal sign to appear more than once in the input .

        If e.KeyChar = "=" Then

            If txtOneUnknownEquation.Text.Length = 0 Or InStr(txtOneUnknownEquation.Text, "=") <> 0 Then

                e.KeyChar = ""
                Exit Sub

            End If

        End If


        'The following If statement does not allow each of the signs
        '+ * / . to be the leftmost character in the input .

        If e.KeyChar = "+" Or e.KeyChar = "*" Or e.KeyChar = "/" Or e.KeyChar = "." Then

            If txtOneUnknownEquation.Text.Length = 0 Then

                e.KeyChar = ""
                Exit Sub

            End If

        End If


        'The following If statement does not allow combinations such as
        '+= -= *= =+ =/ .. +. .+ -+ ++ .

        If e.KeyChar = "=" Or e.KeyChar = "+" Or e.KeyChar = "*" Or e.KeyChar = "/" Or e.KeyChar = "." Then

            If txtOneUnknownEquation.Text.LastIndexOf("=") = txtOneUnknownEquation.Text.Length - 1 Or txtOneUnknownEquation.Text.LastIndexOf("+") = txtOneUnknownEquation.Text.Length - 1 Or txtOneUnknownEquation.Text.LastIndexOf("-") = txtOneUnknownEquation.Text.Length - 1 Or txtOneUnknownEquation.Text.LastIndexOf("*") = txtOneUnknownEquation.Text.Length - 1 Or txtOneUnknownEquation.Text.LastIndexOf("/") = txtOneUnknownEquation.Text.Length - 1 Or txtOneUnknownEquation.Text.LastIndexOf(".") = txtOneUnknownEquation.Text.Length - 1 Then

                e.KeyChar = ""
                Exit Sub

            End If

        End If


        'The following If statement does not allow the combinations
        '+- -- *- /- .-  , but allows the combination =- .
        'The minus sign -  can be the leftmost character in the input,
        'therefore we added the following check here :
        'txtOneUnknownEquation.Text.Length > 0 .

        If txtOneUnknownEquation.Text.Length > 0 Then

            If e.KeyChar = "-" Then

                If txtOneUnknownEquation.Text.LastIndexOf("+") = txtOneUnknownEquation.Text.Length - 1 Or txtOneUnknownEquation.Text.LastIndexOf("-") = txtOneUnknownEquation.Text.Length - 1 Or txtOneUnknownEquation.Text.LastIndexOf("*") = txtOneUnknownEquation.Text.Length - 1 Or txtOneUnknownEquation.Text.LastIndexOf("/") = txtOneUnknownEquation.Text.Length - 1 Or txtOneUnknownEquation.Text.LastIndexOf(".") = txtOneUnknownEquation.Text.Length - 1 Then

                    e.KeyChar = ""
                    Exit Sub

                End If

            End If

        End If


        'Disables key presses for any key , which is not one of the
        'following legal keys :
        'digits (0 to 9) , decimal point "." , "+" , "-" , "*" , "/" ,
        '"x" , "X" , equal sign "=" .

        If Not IsNumeric(e.KeyChar) And e.KeyChar <> "." And e.KeyChar <> "+" And e.KeyChar <> "-" And e.KeyChar <> "*" And e.KeyChar <> "/" And LCase(e.KeyChar) <> "x" And e.KeyChar <> "=" Then

            e.KeyChar = ""

        End If

    End Sub

End Class
