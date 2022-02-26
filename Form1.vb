'Copyright(C) <2017>  <DjDarkBoyZ>

'    This program Is free software: you can redistribute it And/Or modify
'    it under the terms Of the GNU General Public License As published by
'    the Free Software Foundation, either version 3 Of the License, Or
'    (at your option) any later version.

'    This program Is distributed In the hope that it will be useful,
'    but WITHOUT ANY WARRANTY; without even the implied warranty Of
'    MERCHANTABILITY Or FITNESS FOR A PARTICULAR PURPOSE.  See the
'    GNU General Public License For more details.

'    You should have received a copy Of the GNU General Public License
'    along with this program.  If Not, see < http: //www.gnu.org/licenses/>.

Public Class Form1

    Public resultArray() As Byte

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        addLog("Mir2/Mir3 GSP/Kor Dat editor by DjDarkBoyZ", Color.Red)
        addLog("App started", Color.Red)

    End Sub

    Public Sub addLog(ByVal Text As String, ByVal Mode As Color)

        txtLog.SelectionStart = Len(txtLog.Text)

        txtLog.SelectionColor = Mode

        txtLog.SelectedText = Text & vbCrLf
        txtLog.SelectionStart = Len(txtLog.Text)
        txtLog.ScrollToCaret()

        Application.DoEvents()

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        OpenFileDialog1.Filter = "Wemade Encoded Dat Files(*.dat)|*.dat"

        If (OpenFileDialog1.ShowDialog) = DialogResult.OK Then

            If IO.File.Exists(OpenFileDialog1.FileName) Then

                'This is my decode-encode library
                Dim tLib As WemadeCrypt.WemadeCrypt = New WemadeCrypt.WemadeCrypt

                addLog("Decoding -> " & OpenFileDialog1.FileName, Color.Blue)

                Try
                    'Here read all the bytes from the file and decode, then copy the decode bytes to the fileArray
                    Dim fileArray() As Byte = tLib.DecodeBytes(IO.File.ReadAllBytes(OpenFileDialog1.FileName))
                    resultArray = fileArray
                Catch ex As Exception
                    MsgBox(ex.Message)

                    Return
                End Try

                If resultArray.Length = 0 Then
                    Label1.Text = "The file has some problem...check for access rights"

                    Return
                End If

                addLog("Decoded OK ;)", Color.Blue)

                IO.File.WriteAllBytes(OpenFileDialog1.FileName.Substring(0, OpenFileDialog1.FileName.Length - 3) & "xml", resultArray)

                addLog("Saved to: " & OpenFileDialog1.FileName.Substring(0, OpenFileDialog1.FileName.Length - 3) & "xml", Color.Blue)

                Label1.Text = "Decoded -> Saved: " & OpenFileDialog1.FileName.Substring(0, OpenFileDialog1.FileName.Length - 3) & "xml"

            Else

                addLog("File not exists or not have access rights -> " & OpenFileDialog1.FileName, Color.Red)

            End If

        End If

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        OpenFileDialog1.Filter = "Wemade Decoded Xml Files(*.xml)|*.xml"

        If (OpenFileDialog1.ShowDialog) = DialogResult.OK Then

            If resultArray.Length = 0 Then
                Label1.Text = "The file in memory has some problem..."

                Return
            End If

            'This is my decode-encode library
            Dim tLib As WemadeCrypt.WemadeCrypt = New WemadeCrypt.WemadeCrypt

            addLog("Encoding -> " & OpenFileDialog1.FileName, Color.Blue)
            Application.DoEvents()

            'Here copy the decoded string and encode to byte array
            Dim fileArray() As Byte = tLib.EncodeBytes(IO.File.ReadAllBytes(OpenFileDialog1.FileName))

            addLog("Encoded OK ;)", Color.Blue)

            Dim fileName As String = OpenFileDialog1.FileName.Substring(0, OpenFileDialog1.FileName.Length - 3) & "dat"

            IO.File.WriteAllBytes(fileName, fileArray)

            addLog("Saved to: " & fileName, Color.Blue)

            Label1.Text = "Encoded -> Saved: " & fileName

        End If


    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

        AboutBox.ShowDialog()

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click

        Process.Start(My.Resources.URL2)

    End Sub
End Class
