Imports Tinker
Imports Tinker.WC3
Imports Tinker.WC3.Replay
Imports Tinker.Pickling
Imports Strilbrary.Collections
Imports Strilbrary.Values
Imports System.Diagnostics.Contracts

Public Class FilterControl
    Private _ignoreFilterEvents As Boolean
    Public Event FilterChanged(ByVal sender As FilterControl)

    Private Sub OnControlLoad() Handles Me.Load
        _ignoreFilterEvents = True
        For Each e In (From v In EnumValues(Of WC3.Protocol.GameActionId)() Order By v.ToString)
            lscActionTypes.Items.Add(e, isChecked:=True)
        Next e
        For Each e In (From v In EnumValues(Of ReplayEntryId)() Order By v.ToString)
            lscEntryTypeFilter.Items.Add(e, isChecked:=e <> ReplayEntryId.GameStateChecksum)
        Next e
    End Sub

    Public Sub LoadReplay(ByVal replay As ReplayReader)
        Contract.Requires(replay IsNot Nothing)
        Try
            _ignoreFilterEvents = True
            Dim players = (From entry In replay.Entries.Take(25)
                           Where entry.Id = ReplayEntryId.StartOfReplay OrElse entry.Id = ReplayEntryId.PlayerJoined
                           Let vals = DirectCast(entry.Payload.Value, NamedValueMap)
                           Select name = vals.ItemAs(Of String)(If(entry.Id = ReplayEntryId.StartOfReplay, "primary player name", "name")),
                                  pid = vals.ItemAs(Of PlayerId)(If(entry.Id = ReplayEntryId.StartOfReplay, "primary player id", "joiner id"))
                           ).ToList
            If players.Count > 12 Then Throw New IO.InvalidDataException("Replay has too many player entries.")
            If players.Count < 1 Then Throw New IO.InvalidDataException("Replay has no player entries.")

            lscFilterPlayers.Items.Clear()
            For Each player In players
                lscFilterPlayers.Items.Add("{0}: {1}".Frmt(player.pid.Index, player.name), isChecked:=True)
            Next player
        Finally
            _ignoreFilterEvents = False
        End Try
    End Sub

    Private Sub OnFilterChange() Handles txtMinGameTime.TextChanged,
                                         txtMaxGameTime.TextChanged,
                                         chkIgnoreEmptyTicks.CheckedChanged,
                                         lscEntryTypeFilter.ItemCheck,
                                         lscFilterPlayers.ItemCheck,
                                         lscActionTypes.ItemCheck
        If _ignoreFilterEvents Then Return
        RaiseEvent FilterChanged(Me)
    End Sub

    Private Sub OnClickAllActionTypes() Handles btnAllActionTypes.Click
        Try
            _ignoreFilterEvents = True
            For i = 0 To lscActionTypes.Items.Count - 1
                lscActionTypes.SetItemChecked(i, True)
            Next i
        Finally
            _ignoreFilterEvents = False
        End Try
        RaiseEvent FilterChanged(Me)
    End Sub
    Private Sub OnClickNoActionTypes() Handles btnNoActionTypes.Click
        Try
            _ignoreFilterEvents = True
            For i = 0 To lscActionTypes.Items.Count - 1
                lscActionTypes.SetItemChecked(i, False)
            Next i
        Finally
            _ignoreFilterEvents = False
        End Try
        RaiseEvent FilterChanged(Me)
    End Sub

#Region "Generate Filters"
    Private Function TryParseGameTime(ByVal text As String) As UInt32?
        If text Is Nothing Then Return Nothing
        If UInt32.TryParse(text, 0) Then Return UInt32.Parse(text)
        If TimeSpan.TryParse(text, Nothing) Then
            Dim d = TimeSpan.Parse(text).TotalMilliseconds
            If d < 0 Then Return 0UI
            If d > UInt32.MaxValue Then Return UInt32.MaxValue
            Return CUInt(d)
        End If
        Return Nothing
    End Function
    Private ReadOnly Property TimeFilter As Func(Of UInt32, ReplayEntry, Boolean)
        Get
            Dim startTime = TryParseGameTime(txtMinGameTime.Text)
            Dim endTime = TryParseGameTime(txtMaxGameTime.Text)
            Return Function(time As UInt32, entry As ReplayEntry)
                       If startTime IsNot Nothing AndAlso time < startTime Then Return False
                       If endTime IsNot Nothing AndAlso time > endTime Then Return False
                       Return True
                   End Function
        End Get
    End Property
    Private ReadOnly Property PlayerFilter As Func(Of UInt32, ReplayEntry, Boolean)
        Get
            Dim playerPids = (From item In lscFilterPlayers.CheckedItems Select Byte.Parse(item.ToString.Split(":"c)(0))).ToArray
            Return Function(time As UInt32, entry As ReplayEntry)
                       If entry.Id <> ReplayEntryId.Tick Then Return True
                       Dim vals = DirectCast(entry.Payload.Value, NamedValueMap)
                       Dim actions = vals.ItemAs(Of IReadableList(Of Tinker.WC3.Protocol.PlayerActionSet))("player action sets")
                       If actions.Count <= 0 Then Return True
                       Return (From action In actions Where playerPids.Contains(action.Id.Index)).Any
                   End Function
        End Get
    End Property
    Private ReadOnly Property EntryFilter As Func(Of UInt32, ReplayEntry, Boolean)
        Get
            Dim entryTypes = (From item In lscEntryTypeFilter.CheckedItems Select CType(item, ReplayEntryId)).ToArray
            Return Function(time As UInt32, entry As ReplayEntry)
                       Return entryTypes.Contains(entry.Id)
                   End Function
        End Get
    End Property
    Private ReadOnly Property ActionFilter As Func(Of UInt32, ReplayEntry, Boolean)
        Get
            Dim showEmptyActions = Not chkIgnoreEmptyTicks.Checked
            Dim actionTypes = (From item In lscActionTypes.CheckedItems Select CType(item, WC3.Protocol.GameActionId)).ToArray
            Return Function(time As UInt32, entry As ReplayEntry)
                       If entry.Id <> ReplayEntryId.Tick Then Return True
                       Dim vals = DirectCast(entry.Payload.Value, NamedValueMap)
                       Dim actions = vals.ItemAs(Of IReadableList(Of Tinker.WC3.Protocol.PlayerActionSet))("player action sets")
                       If actions.Count <= 0 Then Return showEmptyActions
                       Return (From actionSet In actions
                               From action In actionSet.Actions
                               Where actionTypes.Contains(action.Id)).Any
                   End Function
        End Get
    End Property
    Public ReadOnly Property Filter As Func(Of UInt32, ReplayEntry, Boolean)
        Get
            Dim timeFilter = Me.TimeFilter()
            Dim playerFilter = Me.PlayerFilter()
            Dim entryFilter = Me.PlayerFilter()
            Dim actionFilter = Me.ActionFilter()
            Return Function(time, entry) playerFilter(time, entry) AndAlso
                                         entryFilter(time, entry) AndAlso
                                         timeFilter(time, entry) AndAlso
                                         actionFilter(time, entry)
        End Get
    End Property
#End Region
End Class
