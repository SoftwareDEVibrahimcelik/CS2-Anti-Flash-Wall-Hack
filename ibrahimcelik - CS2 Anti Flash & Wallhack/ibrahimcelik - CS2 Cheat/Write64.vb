Imports System.Runtime.InteropServices
Imports System
Module ReadWritingMemory
    Private Declare Function OpenProcess Lib "kernel32" (ByVal dwDesiredAccess As Integer, ByVal bInheritHandle As Integer, ByVal dwProcessId As Integer) As Integer
    Private Declare Function WriteProcessMemory1 Lib "kernel32" Alias "WriteProcessMemory" (ByVal hProcess As IntPtr, ByVal lpBaseAddress As UInt64, ByRef lpBuffer As UInt64, ByVal nSize As Integer, ByRef lpNumberOfBytesWritten As Integer) As Integer
    Private Declare Function WriteProcessMemory2 Lib "kernel32" Alias "WriteProcessMemory" (ByVal hProcess As IntPtr, ByVal lpBaseAddress As UInt64, ByRef lpBuffer As Single, ByVal nSize As Integer, ByRef lpNumberOfBytesWritten As Integer) As Single
    Private Declare Function WriteProcessMemory3 Lib "kernel32" Alias "WriteProcessMemory" (ByVal hProcess As IntPtr, ByVal lpBaseAddress As UInt64, ByRef lpBuffer As UInt64, ByVal nSize As Integer, ByRef lpNumberOfBytesWritten As Integer) As Long
    Private Declare Function WriteProcessMemory4 Lib "kernel32" Alias "WriteProcessMemory" (ByVal hProcess As IntPtr, ByVal lpBaseAddress As UInt64, ByRef lpBuffer As UInt64, ByVal nSize As Integer, ByRef lpNumberOfBytesWritten As Integer) As UInt64

    Private Declare Function ReadProcessMemory1 Lib "kernel32" Alias "ReadProcessMemory" (ByVal hProcess As IntPtr, ByVal lpBaseAddress As UInt64, ByRef lpBuffer As UInt64, ByVal nSize As Integer, ByRef lpNumberOfBytesWritten As Integer) As Integer
    Private Declare Function ReadProcessMemory2 Lib "kernel32" Alias "ReadProcessMemory" (ByVal hProcess As IntPtr, ByVal lpBaseAddress As UInt64, ByRef lpBuffer As Single, ByVal nSize As Integer, ByRef lpNumberOfBytesWritten As Integer) As Single
    Private Declare Function ReadProcessMemory3 Lib "kernel32" Alias "ReadProcessMemory" (ByVal hProcess As IntPtr, ByVal lpBaseAddress As UInt64, ByRef lpBuffer As UInt64, ByVal nSize As Integer, ByRef lpNumberOfBytesWritten As Integer) As Long
    Private Declare Function ReadProcessMemory4 Lib "kernel32" Alias "ReadProcessMemory" (ByVal hProcess As IntPtr, ByVal lpBaseAddress As UInt64, ByRef lpBuffer As UInt64, ByVal nSize As Integer, ByRef lpNumberOfBytesWritten As Integer) As UInt64

    Const PROCESS_ALL_ACCESS = &H1F0FF

    Private Function UnicodeBytesToString(
    ByVal bytes() As Byte) As String
        'hileyi yapan insta: ibrahim_celik53
        Return System.Text.Encoding.Unicode.GetString(bytes)
    End Function


    'hileyi yapan insta: ibrahim_celik53

    Public Function WriteDMAInteger(ByVal Process As String, ByVal Address As UInt64, ByVal Offsets As UInt64(), ByVal Value As UInt64, ByVal Level As Integer, Optional ByVal nsize As Integer = 8) As Boolean
        Try
            Dim lvl As UInt64 = Address
            For i As Integer = 1 To Level
                lvl = ReadInteger(Process, lvl, nsize) + Offsets(i - 1)
            Next
            WriteInteger(Process, lvl, Value, nsize)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function ReadDMAInteger(ByVal Process As String, ByVal Address As UInt64, ByVal Offsets As UInt64(), ByVal Level As Integer, Optional ByVal nsize As Integer = 8) As UInt64
        Try
            Dim lvl As UInt64 = Address
            For i As Integer = 1 To Level
                lvl = ReadInteger(Process, lvl, nsize) + Offsets(i - 1)
            Next
            Dim vBuffer As UInt32
            vBuffer = ReadInteger(Process, lvl, nsize)
            Return vBuffer
        Catch ex As Exception

        End Try
    End Function

    '
    '
    '
















    Public Function WriteDMAFloat(ByVal Process As String, ByVal Address As UInt64, ByVal Offsets As UInt64(), ByVal Value As Single, ByVal Level As Integer, Optional ByVal nsize As Integer = 8) As Boolean
        Try
            Dim lvl As UInt64 = Address
            For i As Integer = 1 To Level
                lvl = ReadInteger(Process, lvl, nsize) + Offsets(i - 1)
            Next
            WriteFloat(Process, lvl, Value, nsize)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function



    Public Function ReadDMAFloat(ByVal Process As String, ByVal Address As UInt64, ByVal Offsets As UInt64(), ByVal Level As Integer, Optional ByVal nsize As Integer = 8) As Boolean
        Try
            Dim lvl As UInt64 = Address
            For i As Integer = 1 To Level
                lvl = ReadFloat(Process, lvl, nsize) + Offsets(i - 1)
            Next
            Dim vBuffer As Single
            vBuffer = ReadFloat(Process, lvl, nsize)
            Return vBuffer
        Catch ex As Exception

        End Try
    End Function

    Public Function WriteDMALong(ByVal Process As String, ByVal Address As UInt64, ByVal Offsets As Integer(), ByVal Value As Long, ByVal Level As Integer, Optional ByVal nsize As Integer = 8) As Boolean
        Try
            Dim lvl As UInt64 = Address
            For i As Integer = 1 To Level
                lvl = ReadLong(Process, lvl, nsize) + Offsets(i - 1)
            Next
            WriteLong(Process, lvl, Value, nsize)

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    'hileyi yapan insta: ibrahim_celik53
    Public Function ReadDMALong(ByVal Process As String, ByVal Address As UInt64, ByVal Offsets As Integer(), ByVal Level As Integer, Optional ByVal nsize As Integer = 8) As Long
        'hileyi yapan insta: ibrahim_celik53
        Try
            Dim lvl As UInt64 = Address
            For i As Integer = 1 To Level
                lvl = ReadLong(Process, lvl, nsize) + Offsets(i - 1)
            Next
            Dim vBuffer As UInt64
            vBuffer = ReadLong(Process, lvl, nsize)
            Return vBuffer
        Catch ex As Exception

        End Try
    End Function

    Public Sub WriteNOPs(ByVal ProcessName As String, ByVal Address As UInt64, ByVal NOPNum As Integer)
        On Error Resume Next
        Dim C As Integer
        Dim B As Integer
        If ProcessName.EndsWith(".exe") Then
            ProcessName = ProcessName.Replace(".exe", "")
        End If
        Dim MyP As Process() = Process.GetProcessesByName(ProcessName)
        Dim hProcess As IntPtr = OpenProcess(PROCESS_ALL_ACCESS, 0, MyP(0).Id)

        B = 0
        For C = 1 To NOPNum
            Call WriteProcessMemory1(hProcess, Address + B, &H90, 1, 0&)
            B = B + 1
        Next C
        Resume
    End Sub

    Public Sub WriteXBytes(ByVal ProcessName As String, ByVal Address As UInt64, ByVal Value As String)
        On Error Resume Next
        If ProcessName.EndsWith(".exe") Then
            ProcessName = ProcessName.Replace(".exe", "")
        End If
        Dim MyP As Process() = Process.GetProcessesByName(ProcessName)
        Dim hProcess As IntPtr = OpenProcess(PROCESS_ALL_ACCESS, 0, MyP(0).Id)


        Dim C As Integer
        Dim B As Integer
        Dim D As Integer
        Dim V As Byte

        B = 0
        D = 1
        For C = 1 To Math.Round((Len(Value) / 2))
            V = Val("&H" & Mid$(Value, D, 2))
            Call WriteProcessMemory1(hProcess, Address + B, V, 1, 0&)
            B = B + 1
            D = D + 2
        Next C
        Resume
    End Sub

    Public Sub WriteInteger(ByVal ProcessName As String, ByVal Address As UInt64, ByVal Value As Integer, Optional ByVal nsize As Integer = 8)
        On Error Resume Next
        If ProcessName.EndsWith(".exe") Then
            ProcessName = ProcessName.Replace(".exe", "")
        End If
        Dim MyP As Process() = Process.GetProcessesByName(ProcessName)
        Dim hProcess As IntPtr = OpenProcess(PROCESS_ALL_ACCESS, 0, MyP(0).Id)

        Dim vBuffer As UInt64
        Dim hAddress As UInt64
        hAddress = Address
        vBuffer = Value
        WriteProcessMemory1(hProcess, hAddress, CInt(vBuffer), nsize, 0)
        Resume
    End Sub

    Public Sub WriteInt64(ByVal ProcessName As String, ByVal Address As UInt64, ByVal Value As Integer, Optional ByVal nsize As Integer = 8)
        On Error Resume Next
        If ProcessName.EndsWith(".exe") Then
            ProcessName = ProcessName.Replace(".exe", "")
        End If
        Dim MyP As Process() = Process.GetProcessesByName(ProcessName)
        Dim hProcess As IntPtr = OpenProcess(PROCESS_ALL_ACCESS, 0, MyP(0).Id)

        Dim vBuffer As UInt64
        Dim hAddress As UInt64
        hAddress = Address
        vBuffer = Value
        WriteProcessMemory4(hProcess, hAddress, CInt(vBuffer), nsize, 0)
        Resume
    End Sub

    Public Sub WriteFloat(ByVal ProcessName As String, ByVal Address As UInt64, ByVal Value As Single, Optional ByVal nsize As Integer = 8)
        On Error Resume Next
        If ProcessName.EndsWith(".exe") Then
            ProcessName = ProcessName.Replace(".exe", "")
        End If
        Dim MyP As Process() = Process.GetProcessesByName(ProcessName)
        Dim hProcess As IntPtr = OpenProcess(PROCESS_ALL_ACCESS, 0, MyP(0).Id)

        'hileyi yapan insta: ibrahim_celik53
        Dim hAddress As UInt64
        Dim vBuffer As Single

        hAddress = Address
        vBuffer = Value
        WriteProcessMemory2(hProcess, hAddress, vBuffer, nsize, 0)
        Resume
    End Sub

    Public Sub WriteLong(ByVal ProcessName As String, ByVal Address As UInt64, ByVal Value As Long, Optional ByVal nsize As Integer = 8)
        On Error Resume Next
        If ProcessName.EndsWith(".exe") Then
            ProcessName = ProcessName.Replace(".exe", "")
        End If
        Dim MyP As Process() = Process.GetProcessesByName(ProcessName)
        Dim hProcess As IntPtr = OpenProcess(PROCESS_ALL_ACCESS, 0, MyP(0).Id)

        Dim hAddress As UInt64
        Dim vBuffer As Uint64

        hAddress = Address
        vBuffer = Value
        WriteProcessMemory3(hProcess, hAddress, vBuffer, nsize, 0)
        Resume
    End Sub

    Public Function ReadInteger(ByVal ProcessName As String, ByVal Address As UInt64, ByVal Value As UInt64, Optional ByVal nsize As Integer = 8) As UInt64
        On Error Resume Next
        If ProcessName.EndsWith(".exe") Then
            ProcessName = ProcessName.Replace(".exe", "")
        End If
        Dim MyP As Process() = Process.GetProcessesByName(ProcessName)
        Dim hProcess As IntPtr = OpenProcess(PROCESS_ALL_ACCESS, 0, MyP(0).Id)
        Dim vBuffer As UInt64
        Dim hAddress As UInt64
        hAddress = Address
        vBuffer = Value


        ReadProcessMemory1(hProcess, hAddress, vBuffer, nsize, 0)
        Return vBuffer
        Resume
    End Function


    Public Function ReadInt64(ByVal ProcessName As String, ByVal Address As UInt64, Optional ByVal nsize As Integer = 8) As UInt64
        On Error Resume Next
        If ProcessName.EndsWith(".exe") Then
            ProcessName = ProcessName.Replace(".exe", "")
        End If
        Dim MyP As Process() = Process.GetProcessesByName(ProcessName)
        Dim hProcess As IntPtr = OpenProcess(PROCESS_ALL_ACCESS, 0, MyP(0).Id)

        Dim vBuffer As UInt64
        Dim hAddress As UInt64
        hAddress = Address
        ReadProcessMemory4(hProcess, hAddress, vBuffer, nsize, 0)
        Return vBuffer
        Resume
    End Function

    Public Function ReadFloat(ByVal ProcessName As String, ByVal Address As UInt64, Optional ByVal nsize As Integer = 8) As Single
        On Error Resume Next
        If ProcessName.EndsWith(".exe") Then
            ProcessName = ProcessName.Replace(".exe", "")
        End If
        Dim MyP As Process() = Process.GetProcessesByName(ProcessName)
        Dim hProcess As IntPtr = OpenProcess(PROCESS_ALL_ACCESS, 0, MyP(0).Id)
        Dim hAddress As UInt64
        Dim vBuffer As Single

        hAddress = Address
        ReadProcessMemory2(hProcess, hAddress, vBuffer, nsize, 0)
        Return vBuffer
        Resume
    End Function

    Public Function ReadLong(ByVal ProcessName As String, ByVal Address As UInt64, Optional ByVal nsize As Integer = 8) As Long
        On Error Resume Next
        If ProcessName.EndsWith(".exe") Then
            ProcessName = ProcessName.Replace(".exe", "")
        End If
        Dim MyP As Process() = Process.GetProcessesByName(ProcessName)
        Dim hProcess As IntPtr = OpenProcess(PROCESS_ALL_ACCESS, 0, MyP(0).Id)

        Dim hAddress As UInt64
        Dim vBuffer As UInt64

        hAddress = Address
        ReadProcessMemory3(hProcess, hAddress, vBuffer, nsize, 0)
        Return vBuffer
        Resume
    End Function

    '
    '
    '












End Module