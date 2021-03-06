﻿Imports System.IO
Imports System.Diagnostics
Imports System.Threading

Public Class Form1

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Task.Run(Sub() runCommands())

    End Sub

    Private Sub UpdateTextBox(ByVal a As String)
        If Me.InvokeRequired Then
            Dim args() As String = {a}
            Me.Invoke(New Action(Of String)(AddressOf UpdateTextBox), args)
            Return
        End If
        TextBox1.Text += (a & Environment.NewLine)

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
    End Function

    Private Sub runCommands()
        Dim executable As String() = {"adb", "adb"}
        Dim arguments As String() = {"help", "reboot"}
        For i As Integer = 0 To arguments.Length - 1

            Dim process = New Process()
            process.StartInfo = createStartInfo(executable(i), arguments(i))
            process.EnableRaisingEvents = True
            AddHandler process.Exited, Sub(ByVal sendera As Object, ByVal ea As System.EventArgs)
                                           Console.WriteLine(process.ExitTime)
                                           Console.WriteLine(". Processing done.")
                                           UpdateTextBox(Environment.NewLine & process.ExitTime & Environment.NewLine & ".ProcessingDone")

                                       End Sub
            ' catch standard output
            AddHandler process.OutputDataReceived, Sub(ByVal senderb As Object, ByVal eb As DataReceivedEventArgs)
                                                       If (Not String.IsNullOrEmpty(eb.Data)) Then
                                                           Dim b As String = String.Format("{0}> {1}", DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss"), eb.Data)
                                                           Console.WriteLine(b)
                                                           UpdateTextBox(b)

                                                       End If
                                                   End Sub
            ' catch errors
            AddHandler process.ErrorDataReceived, Sub(ByVal senderc As Object, ByVal ec As DataReceivedEventArgs)
                                                      Dim a As String = String.Format("! {0}", ec.Data)
                                                      Console.WriteLine(a)
                                                      UpdateTextBox(a)
                                                  End Sub
            ' start process
            Dim result = process.Start()
            ' and wait for output
            process.BeginOutputReadLine()
            ' and wait for errors :-)
            process.BeginErrorReadLine()
            'process.WaitForExit()
            process.WaitForExit()

        Next

    End Sub

End Class