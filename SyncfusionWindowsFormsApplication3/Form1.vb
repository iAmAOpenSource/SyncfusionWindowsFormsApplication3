Imports System.IO
Imports System.Diagnostics

Public Class Form1

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ' store error output lines

        Dim executable As String() = {"adb", "adb"}
        Dim arguments As String() = {"adb help", " adb reboot"}
        For i As Integer = 0 To arguments.Length - 1
            MsgBox(adb(arguments(i)))
            'Dim process = New Process()
            'process.StartInfo = createStartInfo(executable(i), arguments(i))
            'process.EnableRaisingEvents = True
            'AddHandler process.Exited, Sub(ByVal sendera As Object, ByVal ea As System.EventArgs)
            '                               Console.WriteLine(process.ExitTime)
            '                               Console.WriteLine(". Processing done.")
            '                               'UpdateTextBox(ea)

            '                           End Sub
            '' catch standard output
            'AddHandler process.OutputDataReceived, Sub(ByVal senderb As Object, ByVal eb As DataReceivedEventArgs)
            '                                           If (Not String.IsNullOrEmpty(eb.Data)) Then
            '                                               Console.WriteLine(String.Format("{0}> {1}", DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss"), eb.Data))
            '                                               'UpdateTextBox(eb.Data)

            '                                           End If
            '                                       End Sub
            '' catch errors
            'AddHandler process.ErrorDataReceived, Sub(ByVal senderc As Object, ByVal ec As DataReceivedEventArgs)
            '                                          Console.WriteLine(String.Format("! {0}", ec.Data))
            '                                          Dim a As String = String.Format("! {0}", ec.Data)
            '                                          'UpdateTextBox(a)
            '                                      End Sub
            '' start process
            'Dim result = process.Start()
            '' and wait for output
            'process.BeginOutputReadLine()
            '' and wait for errors :-)
            'process.BeginErrorReadLine()
            'process.WaitForExit()

        Next

    End Sub

    Private Sub UpdateTextBox(ByVal a As String)
        If Me.InvokeRequired Then
            Dim args() As String = {a}
            Me.Invoke(New Action(Of String)(AddressOf UpdateTextBox), args)
            Return
        End If
        Label1.Text += "a"
    End Sub

    Private Function createStartInfo(ByVal executable As String, ByVal arguments As String) As ProcessStartInfo
        Dim processStartInfo = New ProcessStartInfo(executable, arguments)
        processStartInfo.WorkingDirectory = Path.GetDirectoryName(executable)
        ' we want to read standard output
        processStartInfo.RedirectStandardOutput = True
        ' we want to read the standard error
        processStartInfo.RedirectStandardError = True
        processStartInfo.UseShellExecute = False
        processStartInfo.ErrorDialog = False
        processStartInfo.CreateNoWindow = True
        Return processStartInfo
        aaa
    End Function

    Function adb(ByVal Arguments As String) As String
        Try

            Dim My_Process As New Process()
            Dim My_Process_Info As New ProcessStartInfo()

            My_Process_Info.FileName = "cmd.exe" ' Process filename
            My_Process_Info.Arguments = Arguments ' Process arguments
            'My_Process_Info.WorkingDirectory = "C:\Users\<Your User Name>\AppData\Local\Android\android-sdk\platform-tools" 'this directory can be different in your case.
            My_Process_Info.CreateNoWindow = True  ' Show or hide the process Window
            My_Process_Info.UseShellExecute = False ' Don't use system shell to execute the process
            My_Process_Info.RedirectStandardOutput = True  '  Redirect (1) Output
            My_Process_Info.RedirectStandardError = True  ' Redirect non (1) Output

            My_Process.EnableRaisingEvents = True ' Raise events
            My_Process.StartInfo = My_Process_Info
            My_Process.Start() ' Run the process NOW

            Dim Process_ErrorOutput As String = My_Process.StandardOutput.ReadToEnd() ' Stores the Error Output (If any)
            Dim Process_StandardOutput As String = My_Process.StandardOutput.ReadToEnd() ' Stores the Standard Output (If any)

            ' Return output by priority
            If Process_ErrorOutput IsNot Nothing Then Return Process_ErrorOutput ' Returns the ErrorOutput (if any)
            If Process_StandardOutput IsNot Nothing Then Return Process_StandardOutput ' Returns the StandardOutput (if any)

        Catch ex As Exception
            Return ex.Message
        End Try

        Return "OK"

    End Function


End Class