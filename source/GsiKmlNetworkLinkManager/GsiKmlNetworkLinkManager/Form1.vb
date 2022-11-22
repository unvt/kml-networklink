Public Class Form1
    Delegate Sub DelegateSetText(ByVal str As String)

    Private SETUP_FILE_NAME As String = "gsikml.config"
    Private DEFAULT_DATA_FOLDER As String = ""
    Private DATA_FOLDER As String = ""
    Private YEAR_LIST As New ArrayList
    Private Conf As New Dictionary(Of String, String)

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        init()
    End Sub

    Private Sub Form1_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If BackgroundWorker2.IsBusy = True Then
            e.Cancel = True
            MsgBox("Webページの更新中は終了できません。", MsgBoxStyle.Exclamation)
        End If
    End Sub

    Private Sub init()
        Dim tempString As String = ""
        Dim layersUrlFilePath As String = ""
        Dim idx As Integer

        Me.Text = "GsiKmlNewworkLinkManager"
        DEFAULT_DATA_FOLDER = Application.StartupPath & "\data"

        '設定値を取得
        readSetup()

        If Conf("DataFolderPath") = "" Then
            If System.IO.Directory.Exists(DEFAULT_DATA_FOLDER) = True Then
                DATA_FOLDER = DEFAULT_DATA_FOLDER
            End If
        Else
            If System.IO.Directory.Exists(Conf("DataFolderPath")) = True Then
                DATA_FOLDER = Conf("DataFolderPath")
            End If
        End If

        'dataフォルダが存在しない場合はエラー終了
        If DATA_FOLDER = "" Then
            MsgBox("dataフォルダが見つかりません。", MsgBoxStyle.Critical)
            Exit Sub
        End If

        'phpの存在を確認
        If System.IO.File.Exists(DATA_FOLDER & "\gsikmlupdater.php") = False Then
            MsgBox("dataフォルダに gsikmlupdater.php が見つかりません。", MsgBoxStyle.Critical)
            Exit Sub
        End If

        'dataGridViewの設定を有効にする
        enableDataGridViewAutoMode(dgvLayers)

        '集計オプションの初期設定
        YEAR_LIST.Clear()
        For Each yearFolderPath As String In System.IO.Directory.GetDirectories(DATA_FOLDER & "\log")
            Dim yearFolderName As String = System.IO.Path.GetFileName(yearFolderPath)
            If IsNumeric(yearFolderName) = True And yearFolderName.Length = 4 Then
                YEAR_LIST.Add(yearFolderName)
            End If
        Next
        YEAR_LIST.Sort()
        If YEAR_LIST.Count > 0 Then
            dtpStart.MinDate = YEAR_LIST.Item(0) & "/1/1"
            dtpStart.MaxDate = Now
            dtpEnd.MinDate = YEAR_LIST.Item(0) & "/1/1"
            dtpEnd.MaxDate = Now
        End If
        toggleCountEnd()

        'layers_url.txtを読み込む
        dgvLayersUrl.Rows.Clear()
        Dim layersUrl As String() = ReadLayersUrl()
        For i As Integer = 0 To layersUrl.Length - 1
            If layersUrl(i).Trim <> "" Then
                idx = dgvLayersUrl.Rows().Add()
                dgvLayersUrl.Rows(idx).Cells(0).Value = layersUrl(i)
            End If
        Next

        ReadLayers()
        ReadDescription()

    End Sub

    Private Sub btnSaveURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveURL.Click
        'layers.txtのURLを保存
        If System.IO.Directory.Exists(DATA_FOLDER) = True Then
            Using sr As New System.IO.StreamWriter(DATA_FOLDER & "\layers_url.txt", False)
                For i As Integer = 0 To dgvLayersUrl.Rows.Count - 2
                    If dgvLayersUrl.Rows(i).Cells(0).Value <> "" Then
                        sr.WriteLine(dgvLayersUrl.Rows(i).Cells(0).Value.ToString.Trim)
                    End If
                Next
            End Using
        Else
            MsgBox("dataフォルダが見つからない為、urlは保存されませんでした。", MsgBoxStyle.Critical)
        End If

        MsgBox("設定を保存しました。", MsgBoxStyle.Information)
    End Sub



    '************************************************
    '
    ' 設定
    '
    '************************************************
   
    Private Sub btnDataFolder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDataFolder.Click
        Dim dlg As FolderBrowserDialog = FolderBrowserDialog1

        dlg.Description = "Dataフォルダを指定してください。"
        dlg.RootFolder = Environment.SpecialFolder.MyComputer
        dlg.ShowNewFolderButton = False

        tbxDataFolder.Text = tbxDataFolder.Text.Trim
        If tbxDataFolder.Text = "" Then
            dlg.SelectedPath = DEFAULT_DATA_FOLDER
        ElseIf System.IO.Directory.Exists(tbxDataFolder.Text) = True Then
            dlg.SelectedPath = tbxDataFolder.Text
        End If

        If dlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            tbxDataFolder.Text = dlg.SelectedPath
        End If

    End Sub

    Private Sub btnHtmlFolder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHtmlFolder.Click
        Dim dlg As FolderBrowserDialog = FolderBrowserDialog1

        dlg.Description = "Webページフォルダを指定してください。"
        dlg.RootFolder = Environment.SpecialFolder.MyComputer
        dlg.ShowNewFolderButton = False

        tbxHtmlFolder.Text = tbxHtmlFolder.Text.Trim
        If tbxHtmlFolder.Text <> "" AndAlso System.IO.Directory.Exists(tbxHtmlFolder.Text) = True Then
            dlg.SelectedPath = tbxHtmlFolder.Text
        End If

        If dlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            tbxHtmlFolder.Text = dlg.SelectedPath
        End If

    End Sub

    Private Sub btnSaveSetup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveSetup.Click
        writeSetup()
        init()
        MsgBox("設定を保存しました。", MsgBoxStyle.Information)
    End Sub

    Private Sub writeSetup()
        Dim xmlFilePath As String = Application.StartupPath & "\" & SETUP_FILE_NAME
        Dim tbl As DataTable = DataSet1.Tables("Conf")
        Dim row As DataRow

        If tbl.Rows.Count = 0 Then
            row = tbl.Rows.Add()
        Else
            row = tbl.Rows(0)
        End If

        row.Item("DataFolderPath") = tbxDataFolder.Text
        row.Item("HtmlFolderPath") = tbxHtmlFolder.Text
        row.Item("KmlFolderUrl") = tbxKmlUrl.Text
        row.Item("InfoLimit") = tbxInfoLimit.Text

        DataSet1.WriteXml(xmlFilePath)
    End Sub

    Private Sub readSetup()
        Dim xmlFilePath As String = Application.StartupPath & "\" & SETUP_FILE_NAME
        If System.IO.File.Exists(xmlFilePath) = True Then
            DataSet1.Clear()
            DataSet1.ReadXml(xmlFilePath)
        End If

        Dim tbl As DataTable = DataSet1.Tables("Conf")
        Conf.Clear()
        For Each clm As DataColumn In tbl.Columns
            Conf.Add(clm.ColumnName, "")
        Next

        If tbl.Rows.Count > 0 Then
            For Each clm As DataColumn In tbl.Columns
                If tbl.Rows(0).Item(clm.ColumnName) Is DBNull.Value Then
                    Conf(clm.ColumnName) = ""
                Else
                    Conf(clm.ColumnName) = tbl.Rows(0).Item(clm.ColumnName)
                End If
            Next
        End If

        If Conf("InfoLimit") = "" OrElse IsNumeric(Conf("InfoLimit")) = False Then
            Conf("InfoLimit") = 10
        End If

        tbxDataFolder.Text = Conf("DataFolderPath")
        tbxHtmlFolder.Text = Conf("HtmlFolderPath")
        tbxKmlUrl.Text = Conf("KmlFolderUrl")
        tbxInfoLimit.Text = Conf("InfoLimit")

        switchDataFolderLabel()
        switchHtmlFolderLabel()
    End Sub
    
    Private Sub tbxDataFolder_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbxDataFolder.TextChanged
        switchDataFolderLabel()
    End Sub

    Private Sub tbxHtmlFolder_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbxHtmlFolder.TextChanged
        switchHtmlFolderLabel()
    End Sub

    Private Sub switchDataFolderLabel()
        Dim path As String = tbxDataFolder.Text
        If path = "" Then path = Application.StartupPath & "\data"

        If System.IO.Directory.Exists(path) = True Then
            lblDataFolder404.Visible = False
        Else
            lblDataFolder404.Visible = True
        End If

    End Sub

    Private Sub switchHtmlFolderLabel()
        lblHtmlFolder404.Visible = Not System.IO.Directory.Exists(tbxHtmlFolder.Text)
    End Sub

    Private Function isSetupReady()
        Dim dataFolderPath As String = Conf("DataFolderPath")
        If dataFolderPath = "" Then dataFolderPath = Application.StartupPath & "\data"

        Dim htmlFolderPath As String = Conf("HtmlFolderPath")

        Dim updaterFilePath As String = DATA_FOLDER & "\gsikmlupdater.php"

        If System.IO.Directory.Exists(dataFolderPath) = True _
                And System.IO.Directory.Exists(htmlFolderPath) = True _
                And System.IO.File.Exists(updaterFilePath) = True _
                And lblDataFolder404.Visible = False _
                And lblHtmlFolder404.Visible = False Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub tabControl1_Selecting(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TabControlCancelEventArgs) Handles tabControl1.Selecting
        e.Cancel = Not isSetupReady()
    End Sub

    Private Sub tbxInfoLimit_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tbxInfoLimit.TextChanged
        If tbxInfoLimit.Text = "" OrElse IsNumeric(tbxInfoLimit.Text) = False Then
            tbxInfoLimit.BackColor = Color.Pink
        Else
            tbxInfoLimit.BackColor = Color.White
        End If

    End Sub



    '************************************************
    '
    ' Webページを更新
    '
    '************************************************

    Private Sub btnUpdateHtml_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdateHtml.Click
        updateHtml()
    End Sub

    Private Sub updateHtml()
        disableAllButtons()
        txtUpdateMsg.Text = ""

        'phpの存在を確認
        If System.IO.File.Exists(DATA_FOLDER & "\gsikmlupdater.php") = False Then
            MsgBox("dataフォルダに gsikmlupdater.php が見つかりません。", MsgBoxStyle.Critical)
            Exit Sub
        End If

        'layers_urlを確認
        Dim layersUrlCount As Integer = 0
        For i As Integer = 0 To dgvLayersUrl.Rows.Count - 2
            If dgvLayersUrl.Rows(i).Cells(0).Value <> "" Then
                layersUrlCount = layersUrlCount + 1
            End If
        Next
        If layersUrlCount = 0 Then
            MsgBox("layers.txt のURLを設定してください。", MsgBoxStyle.Critical)
            Exit Sub
        End If

        BackgroundWorker2.WorkerReportsProgress = True
        BackgroundWorker2.RunWorkerAsync()
    End Sub

    Private Sub BackgroundWorker2_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker2.DoWork
        updateHtmlWork()
    End Sub

    Private Sub updateHtmlWork()
        Dim iii As Integer = 0
        'コマンド発行
        Dim p As New System.Diagnostics.Process()

        p.StartInfo.UseShellExecute = False
        p.StartInfo.RedirectStandardOutput = True
        p.StartInfo.RedirectStandardError = True
        AddHandler p.OutputDataReceived, AddressOf p_OutputDataReceived
        AddHandler p.ErrorDataReceived, AddressOf p_ErrorDataReceived

        p.StartInfo.FileName = _
            System.Environment.GetEnvironmentVariable("ComSpec")
        p.StartInfo.StandardOutputEncoding = System.Text.Encoding.UTF8
        p.StartInfo.RedirectStandardInput = True
        p.StartInfo.CreateNoWindow = True
        p.StartInfo.Arguments = "/c php """ & DATA_FOLDER & "\gsikmlupdater.php"" -p""" & Conf("HtmlFolderPath") & """ -u""" & Conf("KmlFolderUrl") & """ -l" & Conf("InfoLimit")
        p.Start()

        p.BeginOutputReadLine()
        p.BeginErrorReadLine()

        p.WaitForExit()
        p.Close()
    End Sub

    Private Sub p_OutputDataReceived(ByVal sender As Object, _
            ByVal e As System.Diagnostics.DataReceivedEventArgs)
        'Debug.Print(e.Data)
        Invoke(New DelegateSetText(AddressOf setUpdateMsg), e.Data)
    End Sub

    Private Sub p_ErrorDataReceived(ByVal sender As Object, _
            ByVal e As System.Diagnostics.DataReceivedEventArgs)
        'Console.WriteLine("ERR>{0}", e.Data)
        'Debug.Print(e.Data)
        Invoke(New DelegateSetText(AddressOf setUpdateMsg), e.Data)
    End Sub

    Private Sub setUpdateMsg(ByVal str As String)
        If Not (str Is Nothing) AndAlso str <> "" Then
            txtUpdateMsg.AppendText(Trim(str) & vbCrLf)
        End If
    End Sub

    Private Sub BackgroundWorker2_RunWorkerCompleted(ByVal sender As System.Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker2.RunWorkerCompleted

        enableAllButtons()
        MsgBox("Webページを更新しました。", MsgBoxStyle.Information)
    End Sub



    '************************************************
    '
    ' layers_txt のURL
    '
    '************************************************

    Private Sub dgvLayersUrl_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvLayersUrl.CellContentClick
        Dim dgv As DataGridView = CType(sender, DataGridView)
        If dgv.Columns(e.ColumnIndex).Name = "clmUp" Then
            If e.RowIndex >= 1 Then
                Dim temp = dgv.Rows(e.RowIndex - 1).Cells("clmUrl").Value
                dgv.Rows(e.RowIndex - 1).Cells("clmUrl").Value = dgv.Rows(e.RowIndex).Cells("clmUrl").Value
                dgv.Rows(e.RowIndex).Cells("clmUrl").Value = temp
            End If
        ElseIf dgv.Columns(e.ColumnIndex).Name = "clmDown" Then
            If e.RowIndex < dgv.Rows.Count - 2 Then
                Dim temp = dgv.Rows(e.RowIndex + 1).Cells("clmUrl").Value
                dgv.Rows(e.RowIndex + 1).Cells("clmUrl").Value = dgv.Rows(e.RowIndex).Cells("clmUrl").Value
                dgv.Rows(e.RowIndex).Cells("clmUrl").Value = temp
            End If
        End If
    End Sub

    Private Function ReadLayersUrl()
        Dim tempString As String = ""
        Dim filePath As String = DATA_FOLDER & "\layers_url.txt"
        If System.IO.File.Exists(filePath) = True Then
            Using sr As New System.IO.StreamReader(filePath, False)
                tempString = sr.ReadToEnd
            End Using
        End If
        ReadLayersUrl = Split(tempString, vbCrLf)
    End Function

    Private Function ReadLayersIds()
        Dim idList As ArrayList = New ArrayList
        Dim tempString As String = ""
        Dim filePath As String = DATA_FOLDER & "\layers_ids.txt"
        If System.IO.File.Exists(filePath) = True Then
            Using sr As New System.IO.StreamReader(filePath, False)
                tempString = sr.ReadToEnd
            End Using
        End If
        Dim lines As String() = Split(tempString, vbCrLf)
        For i As Integer = 0 To lines.Length - 1
            If lines(i) <> "" Then
                Dim items As String() = Split(lines(i), ",")
                If items.Length > 0 Then
                    Dim id As String = Trim(items(0))
                    If id <> "" Then
                        idList.Add(id)
                    End If
                End If
            End If
        Next
        ReadLayersIds = idList
    End Function

    Private Sub ReadLayers()
        Dim idx As Integer
        Dim layersIds As ArrayList = ReadLayersIds()

        dgvLayers.Rows.Clear()
        For i As Integer = 0 To layersIds.Count - 1
            Dim idString As String = layersIds(i).Trim
            If idString.Trim <> "" Then
                idx = dgvLayers.Rows().Add()
                dgvLayers.Rows(idx).Cells("ColumnReadOrder").Value = i + 1
                dgvLayers.Rows(idx).Cells("ColumnLayerID").Value = idString
                dgvLayers.Rows(idx).Cells("ColumnFileName").Value = "gsi_" & idString & ".kml"
            End If
        Next
    End Sub



    '************************************************
    '
    ' 集計オプション
    '
    '************************************************

    Private Sub onChangeOption()
        dtpStart.Enabled = True
        dtpEnd.Enabled = True

        If rdbYear.Checked = True Then
            dtpStart.CustomFormat = "yyyy年"
            dtpEnd.CustomFormat = "yyyy年"
        ElseIf rdbMonth.Checked = True Then
            dtpStart.CustomFormat = "yyyy年MM月"
            dtpEnd.CustomFormat = "yyyy年MM月"
        ElseIf rdbDay.Checked = True Then
            dtpStart.CustomFormat = "yyyy年MM月dd日"
            dtpEnd.CustomFormat = "yyyy年MM月dd日"
            'Dim tempDate As String = dtpStart.Value.Year & "/" & dtpStart.Value.Month & "/" & Now.Day
            'If IsDate(tempDate) = True Then
            '    dtpStart.Value = tempDate
            'Else
            '    dtpStart.Value = dtpStart.Value.Year & "/" & dtpStart.Value.Month & "/1"
            'End If
        Else
            dtpStart.Enabled = False
            dtpEnd.Enabled = False
        End If
    End Sub

    Private Sub rdbYear_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbYear.CheckedChanged
        onChangeOption()
    End Sub

    Private Sub rdbMonth_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbMonth.CheckedChanged
        onChangeOption()
    End Sub

    Private Sub rdbDay_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbDay.CheckedChanged
        onChangeOption()
    End Sub

    Private Sub rdbTotal_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbTotal.CheckedChanged
        onChangeOption()
    End Sub

    Private Sub dtpStart_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpStart.ValueChanged
        If ckbFree.Checked = False Then
            dtpEnd.Value = dtpStart.Value
        End If
    End Sub

    Private Sub ckbFree_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckbFree.CheckedChanged
        toggleCountEnd()
    End Sub

    Sub toggleCountEnd()
        If ckbFree.Checked = True Then
            dtpEnd.Visible = True
            lblStart.Visible = True
            lblEnd.Visible = True
            grpCount.Width = 700
        Else
            dtpEnd.Visible = False
            lblStart.Visible = False
            lblEnd.Visible = False
            grpCount.Width = 520
        End If
    End Sub



    '************************************************
    '
    ' 集計
    '
    '************************************************

    '集計ボタンクリック
    Private Sub btnCount_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCount.Click
        ReadAccessLog()
    End Sub

    '集計キャンセルクリック
    Private Sub btnCountCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCountCancel.Click
        If BackgroundWorker1.IsBusy = True Then
            BackgroundWorker1.CancelAsync()
        End If
    End Sub

    Private Sub ReadAccessLog()
        Dim cnt As Integer = 0
        Dim readMode As String = ""
        If rdbTotal.Checked = True Then
            readMode = "total"
            cnt = DateDiff(DateInterval.Year, dtpStart.Value, dtpEnd.Value) + 1
        ElseIf rdbYear.Checked = True Then
            readMode = "year"
            cnt = DateDiff(DateInterval.Year, dtpStart.Value, dtpEnd.Value) + 1
        ElseIf rdbMonth.Checked = True Then
            readMode = "month"
            cnt = DateDiff(DateInterval.Month, dtpStart.Value, dtpEnd.Value) + 1
        ElseIf rdbDay.Checked = True Then
            readMode = "day"
            cnt = DateDiff(DateInterval.Day, dtpStart.Value, dtpEnd.Value) + 1
        End If

        If cnt > 366 Then
            MsgBox("366列を超える集計は行えません。", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        disableAllButtons()
        btnCountCancel.Enabled = True
        prbRead.Value = 0

        Dim param As ArrayList = New ArrayList
        param.Add(readMode)
        param.Add(dtpStart.Value)
        If ckbFree.Checked = True Then
            param.Add(dtpEnd.Value)
        Else
            param.Add(dtpStart.Value)
        End If

        BackgroundWorker1.WorkerReportsProgress = True
        BackgroundWorker1.RunWorkerAsync(param)
    End Sub

    Private Sub BackgroundWorker1_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork

        Dim param As ArrayList = e.Argument

        If param(0) = "total" Then
            ReadAccessLogAllWork(sender, e)
        Else
            ReadAccessLogWork(sender, e)
        End If

    End Sub

    Private Sub BackgroundWorker1_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles BackgroundWorker1.ProgressChanged
        prbRead.Value = e.ProgressPercentage
    End Sub

    Private Sub BackgroundWorker1_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted

        If e.Cancelled = False Then
            Dim logDic As Dictionary(Of String, Dictionary(Of String, Integer)) = e.Result(0)
            Dim readStartDate As Date = e.Result(1)
            Dim readEndDate As Date = e.Result(2)

            'Dictionaryにアクセスするkeyをlistへ取得
            Dim keysList As ArrayList = New ArrayList()
            If rdbTotal.Checked = True Then
                For y As Integer = YEAR_LIST(0) To YEAR_LIST(YEAR_LIST.Count - 1)
                    keysList.Add(y.ToString)
                Next
            ElseIf rdbYear.Checked = True Then
                For y As Integer = readStartDate.Year To readEndDate.Year
                    keysList.Add(y.ToString)
                Next
            ElseIf rdbMonth.Checked = True Then
                Dim tempDate As DateTime = readStartDate
                Do While tempDate <= readEndDate
                    Dim mm As String = tempDate.Month.ToString("D2")
                    keysList.Add(tempDate.Year.ToString & "/" & mm)
                    tempDate = DateAdd(DateInterval.Month, 1, tempDate)
                Loop
            ElseIf rdbDay.Checked = True Then
                Dim tempDate As DateTime = readStartDate
                Do While tempDate <= readEndDate
                    Dim mm As String = tempDate.Month.ToString("D2")
                    Dim dd As String = tempDate.Day.ToString("D2")
                    keysList.Add(tempDate.Year.ToString & "/" & mm & "/" & dd)
                    tempDate = DateAdd(DateInterval.Day, 1, tempDate)
                Loop
            End If

            'dataGridViewの自動モードを無効にする
            disableDataGridViewAutoMode(dgvLayers)

            'Columnの再作成
            Dim baseColumnCount As Integer = 5
            dgvLayers.ColumnCount = baseColumnCount + keysList.Count
            For x As Integer = 0 To keysList.Count - 1
                dgvLayers.Columns(x + baseColumnCount).HeaderText = keysList.Item(x).ToString
                dgvLayers.Columns(x + baseColumnCount).Width = 70
                dgvLayers.Columns(x + baseColumnCount).FillWeight = 70
            Next
            Application.DoEvents()

            'Columnへ値を挿入
            For i As Integer = 0 To dgvLayers.Rows.Count - 1
                Dim idString As String = dgvLayers.Rows(i).Cells("ColumnLayerID").Value
                Dim totalCnt As Long = 0

                If idString.Trim = "" Then
                    For j As Integer = 0 To keysList.Count - 1
                        dgvLayers.Rows(i).Cells(j + baseColumnCount).Value = Nothing
                    Next
                Else
                    For j As Integer = 0 To keysList.Count - 1
                        dgvLayers.Rows(i).Cells(j + baseColumnCount).ReadOnly = True
                        Dim dtString As String = keysList.Item(j).ToString
                        If logDic.ContainsKey(idString) = True AndAlso logDic(idString).ContainsKey(dtString) = True Then
                            dgvLayers.Rows(i).Cells(j + baseColumnCount).Value = logDic(idString)(dtString)
                            totalCnt = totalCnt + logDic(idString)(dtString)
                        Else
                            dgvLayers.Rows(i).Cells(j + baseColumnCount).Value = 0
                        End If
                        Application.DoEvents()
                    Next
                End If

                dgvLayers.Rows(i).Cells("ColumnAccessCount").Value = totalCnt

            Next

            'dataGridViewの自動モードを有効にする
            enableDataGridViewAutoMode(dgvLayers)
        End If

        prbRead.Value = 0
        enableAllButtons()
        btnCountCancel.Enabled = False
    End Sub

    Private Sub ReadAccessLogWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs)
        Dim param As ArrayList = e.Argument
        Dim readMode As String = param(0)
        Dim readStartDate As DateTime = param(1)
        Dim readEndDate As DateTime = param(2)

        Dim logFolderPath As String = DATA_FOLDER & "\log"
        Dim logDic As New Dictionary(Of String, Dictionary(Of String, Integer))

        If readMode = "day" Then
            readStartDate = New DateTime(readStartDate.Year, readStartDate.Month, readStartDate.Day)
            readEndDate = New DateTime(readEndDate.Year, readEndDate.Month, readEndDate.Day)
        ElseIf readMode = "month" Then
            readStartDate = New DateTime(readStartDate.Year, readStartDate.Month, 1)
            readEndDate = New DateTime(readEndDate.Year, readEndDate.Month, DateTime.DaysInMonth(readEndDate.Year, readEndDate.Month))
        Else
            readStartDate = New DateTime(readStartDate.Year, 1, 1)
            readEndDate = New DateTime(readEndDate.Year, 12, 31)
        End If

        Dim dayCountTotal As Integer = DateDiff(DateInterval.Day, readStartDate, readEndDate) + 1
        Dim dayCount As Integer = 0
        Dim currentDate As DateTime = readStartDate

        Do While currentDate <= readEndDate
            If BackgroundWorker1.CancellationPending = True Then
                e.Cancel = True
                Exit Do
            End If

            Dim fileName As String = currentDate.ToString("yyyy-MM-dd") & ".txt"
            Dim filePath As String = logFolderPath & "\" & currentDate.Year.ToString & "\" & fileName

            If System.IO.File.Exists(filePath) = True Then
                Dim keyString As String = currentDate.ToString("yyyy")
                If readMode = "month" Then
                    keyString = currentDate.ToString("yyyy/MM")
                ElseIf readMode = "day" Then
                    keyString = currentDate.ToString("yyyy/MM/dd")
                End If

                Using sr As New System.IO.StreamReader(filePath)
                    Dim headers As String() = Split(sr.ReadLine(), ",")
                    Dim dtIndex As Integer = -1
                    Dim idIndex As Integer = -1
                    For i As Integer = 0 To headers.Length - 1
                        If headers(i) = "DATE" Then
                            dtIndex = i
                            Exit For
                        End If
                    Next
                    For i As Integer = 0 To headers.Length - 1
                        If headers(i) = "ID" Then
                            idIndex = i
                            Exit For
                        End If
                    Next

                    If dtIndex >= 0 And idIndex >= 0 Then
                        While Not sr.EndOfStream
                            Dim line As String = sr.ReadLine()
                            If line.Trim = "" Then Continue While

                            Dim row As String() = Split(line, ",")
                            Dim dt As String = row(dtIndex).Trim
                            Dim id As String = row(idIndex).Trim

                            If IsDate(dt) = False OrElse id = "" Then
                                Continue While
                            End If

                            If logDic.ContainsKey(id) = False Then
                                logDic(id) = New Dictionary(Of String, Integer)
                            End If

                            If logDic(id).ContainsKey(keyString) = True Then
                                logDic(id)(keyString) = logDic(id)(keyString) + 1
                            Else
                                logDic(id)(keyString) = 1
                            End If
                        End While
                    End If
                End Using
            End If

            currentDate = DateAdd(DateInterval.Day, 1, currentDate)
            dayCount = dayCount + 1
            BackgroundWorker1.ReportProgress(dayCount / dayCountTotal * 100)
        Loop

        Dim resultList As ArrayList = New ArrayList()
        resultList.Add(logDic)
        resultList.Add(readStartDate)
        resultList.Add(readEndDate)

        e.Result = resultList
    End Sub

    Private Sub ReadAccessLogAllWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs)
        Dim param As ArrayList = e.Argument
        Dim readMode As String = param(0)
        Dim readStartDate As DateTime = param(1)
        Dim readEndDate As DateTime = param(2)

        Dim logFolderPath As String = DATA_FOLDER & "\log"
        Dim logDic As New Dictionary(Of String, Dictionary(Of String, Integer))

        Dim startTime As DateTime = Now()
        Dim yearFolderCount As Integer = 0

        For Each yearFolderPath As String In System.IO.Directory.GetDirectories(logFolderPath)
            If BackgroundWorker1.CancellationPending = True Then
                e.Cancel = True
                Exit For
            End If

            Dim yearFolderName As String = System.IO.Path.GetFileName(yearFolderPath)
            If IsNumeric(yearFolderName) = False Or yearFolderName.Length <> 4 Then
                Continue For
            End If

            For Each filePath As String In System.IO.Directory.GetFiles(yearFolderPath)
                Using sr As New System.IO.StreamReader(filePath, False)
                    Dim headers As String() = Split(sr.ReadLine(), ",")
                    Dim dtIndex As Integer = -1
                    Dim idIndex As Integer = -1
                    For i As Integer = 0 To headers.Length - 1
                        If headers(i) = "DATE" Then
                            dtIndex = i
                            Exit For
                        End If
                    Next
                    For i As Integer = 0 To headers.Length - 1
                        If headers(i) = "ID" Then
                            idIndex = i
                            Exit For
                        End If
                    Next

                    If dtIndex >= 0 And idIndex >= 0 Then
                        While Not sr.EndOfStream
                            Dim line As String = sr.ReadLine()
                            If line.Trim = "" Then Continue While

                            Dim row As String() = Split(line, ",")
                            Dim dt As String = row(dtIndex)
                            Dim id As String = row(idIndex)

                            If IsDate(dt) = False Then
                                Continue While
                            End If

                            Dim dtDate As Date = Date.Parse(dt)
                            Dim yyyy As String = dtDate.Year.ToString
                            Dim mm As String = dtDate.Month.ToString("D2")
                            Dim dd As String = dtDate.Day.ToString("D2")

                            If logDic.ContainsKey(id) = False Then
                                logDic(id) = New Dictionary(Of String, Integer)
                            End If

                            If logDic(id).ContainsKey(yyyy) = True Then
                                logDic(id)(yyyy) = logDic(id)(yyyy) + 1
                            Else
                                logDic(id)(yyyy) = 1
                            End If
                        End While
                    End If
                End Using
            Next
            yearFolderCount = yearFolderCount + 1
            BackgroundWorker1.ReportProgress(yearFolderCount / YEAR_LIST.Count * 100)
        Next

        Dim resultList As ArrayList = New ArrayList()
        resultList.Add(logDic)
        resultList.Add(readStartDate)
        resultList.Add(readEndDate)

        e.Result = resultList
    End Sub



    '************************************************
    '
    ' csv
    '
    '************************************************

    Private Sub btnSaveCsv_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveCsv.Click
        Dim dlg As SaveFileDialog = SaveFileDialog1

        dlg.FileName = "gsikml-" & DateTime.Now.ToString("yyyyMMdd-HHmmss") & ".csv"
        dlg.InitialDirectory = Environment.SpecialFolder.Desktop
        dlg.DefaultExt = ".csv"
        dlg.Filter = "csv Shift-JIS|*.*|csv UTF-8 BOMあり|*.*|csv UTF-8 BOMなし|*.*"
        dlg.FilterIndex = 0
        dlg.Title = "保存先のファイルを選択してください"
        dlg.RestoreDirectory = True
        dlg.OverwritePrompt = True

        If dlg.ShowDialog() <> Windows.Forms.DialogResult.OK Then
            Exit Sub
        End If

        If dlg.FilterIndex = 1 Then
            Using sw As New System.IO.StreamWriter(dlg.FileName, False, System.Text.Encoding.GetEncoding("shift_jis"))
                saveCsv(sw)
            End Using
        ElseIf dlg.FilterIndex = 2 Then
            Using sw As New System.IO.StreamWriter(dlg.FileName, False, System.Text.Encoding.UTF8)
                saveCsv(sw)
            End Using
        Else
            Using sw As New System.IO.StreamWriter(dlg.FileName, False)
                saveCsv(sw)
            End Using
        End If

        MsgBox("csvファイルを保存しました。", MsgBoxStyle.Information)
    End Sub

    Private Sub saveCsv(ByRef sw As System.IO.StreamWriter)
        Dim clmArr As List(Of String) = New List(Of String)
        Dim txt As String = ""

        If 1 = 1 Then
            clmArr.Clear()
            For x As Integer = 1 To dgvLayers.ColumnCount - 1
                txt = dgvLayers.Columns(x).HeaderText
                If txt Is Nothing Then txt = ""
                txt = """" & csvencode(txt.ToString) & """"
                clmArr.Add(txt)
            Next
            sw.WriteLine(String.Join(",", clmArr.ToArray()))
        End If

        For y As Integer = 0 To dgvLayers.RowCount - 1
            clmArr.Clear()
            For x As Integer = 1 To dgvLayers.ColumnCount - 1
                txt = dgvLayers.Rows(y).Cells(x).Value
                If txt Is Nothing Then txt = ""
                txt = """" & csvencode(txt.ToString) & """"
                clmArr.Add(txt)
            Next
            sw.WriteLine(String.Join(",", clmArr.ToArray()))
        Next
    End Sub



    '************************************************
    '
    ' 説明
    '
    '************************************************

    Private Sub btnSaveDesc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveDesc.Click
        Dim errorMessage As String = ""
        disableAllButtons()

        If SaveDescription(errorMessage) = True Then
            ReadDescription()

            If MsgBox("説明を保存しました。" & vbCrLf & vbCrLf & _
                      "続けて「Webページを更新」しますか？", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                tabControl1.SelectTab(tabLayersTxt)
                updateHtml()
                Exit Sub
            End If
        Else
            MsgBox(errorMessage, MsgBoxStyle.Critical)
        End If

        enableAllButtons()
    End Sub

    Private Function SaveDescription(Optional ByRef errorMessage = "") As Boolean
        SaveDescription = False

        Dim descFilePath As String = DATA_FOLDER & "\layers_desc.txt"
        Dim tempFilePath As String = DATA_FOLDER & "\layers_desc.tmp"
        Dim backFilePath As String = DATA_FOLDER & "\layers_desc.bak"
        Dim sep As String = "<___ID_SEPARATOR___>"

        'dataフォルダを確認
        If System.IO.Directory.Exists(DATA_FOLDER) = False Then
            errorMessage = "dataフォルダが見つかりません。"
            Return False
        End If

        Using sw As New System.IO.StreamWriter(tempFilePath, False)
            For i As Integer = 0 To dgvLayers.Rows.Count - 1
                Dim id As String = dgvLayers.Rows(i).Cells("ColumnLayerID").Value
                Dim desc As String = dgvLayers.Rows(i).Cells("ColumnDescription").Value

                If id Is Nothing Then id = ""
                If desc Is Nothing Then desc = ""
                id = id.Trim
                desc = desc.Trim

                If id <> "" And desc <> "" Then
                    sw.WriteLine(id)
                    sw.Write(desc & vbCrLf)
                    sw.WriteLine(sep)
                    sw.Flush()
                End If
            Next
        End Using

        If System.IO.File.Exists(backFilePath) = True Then
            System.IO.File.Delete(backFilePath)
        End If

        If System.IO.File.Exists(descFilePath) = True Then
            System.IO.File.Move(descFilePath, backFilePath)
        End If

        System.IO.File.Move(tempFilePath, descFilePath)

        Return True
    End Function

    Private Function ReadDescription() As Dictionary(Of String, String)
        Dim descDic As Dictionary(Of String, String) = New Dictionary(Of String, String)
        Dim descFilePath As String = DATA_FOLDER & "\layers_desc.txt"
        Dim sep As String = "<___ID_SEPARATOR___>"

        Dim descList As String() = New String() {}

        If System.IO.File.Exists(descFilePath) = True Then
            Using sr As New System.IO.StreamReader(descFilePath)
                descList = Split(sr.ReadToEnd(), sep)
            End Using
        End If

        For i As Integer = 0 To descList.Length - 1
            descList(i) = descList(i).Trim
            If descList(i) <> "" Then
                Dim items As String() = Split(descList(i), vbCrLf)
                Dim key As String = items(0)
                Dim val As String = ""
                For j As Integer = 1 To items.Length - 1
                    val = val & items(j) & vbCrLf
                Next
                descDic(key) = val.Trim
            End If
        Next

        For i As Integer = 0 To dgvLayers.Rows.Count - 1
            Dim idString As String = dgvLayers.Rows(i).Cells("ColumnLayerID").Value
            If idString.Trim <> "" Then
                If descDic.ContainsKey(idString) = True Then
                    dgvLayers.Rows(i).Cells("ColumnDescription").Value = descDic(idString)
                Else
                    dgvLayers.Rows(i).Cells("ColumnDescription").Value = ""
                End If
            End If
        Next

        Return descDic
    End Function



    '************************************************
    '
    ' 汎用
    '
    '************************************************

    Private Function csvencode(ByVal str As String)
        Dim result As String = str
        result = Replace(str, """", """""")
        csvencode = result
    End Function

    Private Sub enableDataGridViewAutoMode(ByRef dgv As DataGridView)
        dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None
        dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
        dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgv.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.EnableResizing
    End Sub

    Private Sub disableDataGridViewAutoMode(ByRef dgv As DataGridView)
        dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None
        dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None
        dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        dgv.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing
    End Sub

    Private Sub disableAllButtons()
        btnSaveURL.Enabled = False
        btnUpdateHtml.Enabled = False
        btnCount.Enabled = False
        'btnCountCancel.Enabled = False
        btnSaveDesc.Enabled = False
        btnSaveCsv.Enabled = False
    End Sub

    Private Sub enableAllButtons()
        btnSaveURL.Enabled = True
        btnUpdateHtml.Enabled = True
        btnCount.Enabled = True
        'btnCountCancel.Enabled = True
        btnSaveDesc.Enabled = True
        btnSaveCsv.Enabled = True
    End Sub



    '************************************************
    '
    ' 絞込検索
    '
    '************************************************
    Private Sub btnFilter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFilter.Click
        For i As Integer = 0 To dgvLayers.RowCount - 1
            Dim showFlg As Boolean = False

            If System.Text.RegularExpressions.Regex.IsMatch( _
                    dgvLayers.Rows(i).Cells("ColumnFileName").Value & "", tbxFilter.Text) Then
                showFlg = True
            ElseIf System.Text.RegularExpressions.Regex.IsMatch( _
                    dgvLayers.Rows(i).Cells("ColumnDescription").Value & "", tbxFilter.Text) Then
                showFlg = True
            End If

            dgvLayers.Rows(i).Visible = showFlg
        Next
    End Sub
End Class
