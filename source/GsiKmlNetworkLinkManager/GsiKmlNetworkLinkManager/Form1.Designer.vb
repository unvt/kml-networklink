<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
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

    'Windows フォーム デザイナで必要です。
    Private components As System.ComponentModel.IContainer

    'メモ: 以下のプロシージャは Windows フォーム デザイナで必要です。
    'Windows フォーム デザイナを使用して変更できます。  
    'コード エディタを使って変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.tabControl1 = New System.Windows.Forms.TabControl
        Me.tabSetup = New System.Windows.Forms.TabPage
        Me.GroupBox6 = New System.Windows.Forms.GroupBox
        Me.tbxInfoLimit = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        Me.tbxKmlUrl = New System.Windows.Forms.TextBox
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.lblHtmlFolder404 = New System.Windows.Forms.Label
        Me.btnHtmlFolder = New System.Windows.Forms.Button
        Me.tbxHtmlFolder = New System.Windows.Forms.TextBox
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.lblDataFolder404 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.btnDataFolder = New System.Windows.Forms.Button
        Me.tbxDataFolder = New System.Windows.Forms.TextBox
        Me.btnSaveSetup = New System.Windows.Forms.Button
        Me.tabLayersTxt = New System.Windows.Forms.TabPage
        Me.GroupBox5 = New System.Windows.Forms.GroupBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.txtUpdateMsg = New System.Windows.Forms.TextBox
        Me.btnUpdateHtml = New System.Windows.Forms.Button
        Me.btnSaveURL = New System.Windows.Forms.Button
        Me.dgvLayersUrl = New System.Windows.Forms.DataGridView
        Me.clmUrl = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.clmUp = New System.Windows.Forms.DataGridViewButtonColumn
        Me.clmDown = New System.Windows.Forms.DataGridViewButtonColumn
        Me.tabLayersList = New System.Windows.Forms.TabPage
        Me.btnSaveDesc = New System.Windows.Forms.Button
        Me.btnCountCancel = New System.Windows.Forms.Button
        Me.prbRead = New System.Windows.Forms.ProgressBar
        Me.grpCount = New System.Windows.Forms.GroupBox
        Me.ckbFree = New System.Windows.Forms.CheckBox
        Me.btnCount = New System.Windows.Forms.Button
        Me.lblEnd = New System.Windows.Forms.Label
        Me.lblStart = New System.Windows.Forms.Label
        Me.dtpEnd = New System.Windows.Forms.DateTimePicker
        Me.rdbTotal = New System.Windows.Forms.RadioButton
        Me.dtpStart = New System.Windows.Forms.DateTimePicker
        Me.rdbDay = New System.Windows.Forms.RadioButton
        Me.rdbMonth = New System.Windows.Forms.RadioButton
        Me.rdbYear = New System.Windows.Forms.RadioButton
        Me.btnSaveCsv = New System.Windows.Forms.Button
        Me.dgvLayers = New System.Windows.Forms.DataGridView
        Me.ColumnReadOrder = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ColumnLayerID = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ColumnFileName = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ColumnDescription = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ColumnAccessCount = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataSet1 = New System.Data.DataSet
        Me.dtsConf = New System.Data.DataTable
        Me.DataColumn1 = New System.Data.DataColumn
        Me.DataColumn2 = New System.Data.DataColumn
        Me.DataColumn3 = New System.Data.DataColumn
        Me.DataColumn4 = New System.Data.DataColumn
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker
        Me.BackgroundWorker2 = New System.ComponentModel.BackgroundWorker
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog
        Me.tbxFilter = New System.Windows.Forms.TextBox
        Me.btnFilter = New System.Windows.Forms.Button
        Me.tabControl1.SuspendLayout()
        Me.tabSetup.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.tabLayersTxt.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        CType(Me.dgvLayersUrl, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabLayersList.SuspendLayout()
        Me.grpCount.SuspendLayout()
        CType(Me.dgvLayers, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataSet1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtsConf, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'tabControl1
        '
        Me.tabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tabControl1.Controls.Add(Me.tabSetup)
        Me.tabControl1.Controls.Add(Me.tabLayersTxt)
        Me.tabControl1.Controls.Add(Me.tabLayersList)
        Me.tabControl1.Location = New System.Drawing.Point(3, 4)
        Me.tabControl1.Name = "tabControl1"
        Me.tabControl1.SelectedIndex = 0
        Me.tabControl1.Size = New System.Drawing.Size(779, 456)
        Me.tabControl1.TabIndex = 11
        '
        'tabSetup
        '
        Me.tabSetup.Controls.Add(Me.GroupBox6)
        Me.tabSetup.Controls.Add(Me.GroupBox4)
        Me.tabSetup.Controls.Add(Me.GroupBox3)
        Me.tabSetup.Controls.Add(Me.GroupBox2)
        Me.tabSetup.Controls.Add(Me.btnSaveSetup)
        Me.tabSetup.Location = New System.Drawing.Point(4, 22)
        Me.tabSetup.Name = "tabSetup"
        Me.tabSetup.Padding = New System.Windows.Forms.Padding(3)
        Me.tabSetup.Size = New System.Drawing.Size(771, 430)
        Me.tabSetup.TabIndex = 2
        Me.tabSetup.Text = "設定"
        Me.tabSetup.UseVisualStyleBackColor = True
        '
        'GroupBox6
        '
        Me.GroupBox6.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox6.Controls.Add(Me.tbxInfoLimit)
        Me.GroupBox6.Controls.Add(Me.Label5)
        Me.GroupBox6.Location = New System.Drawing.Point(6, 299)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(759, 48)
        Me.GroupBox6.TabIndex = 12
        Me.GroupBox6.TabStop = False
        Me.GroupBox6.Text = "更新履歴保持日数"
        '
        'tbxInfoLimit
        '
        Me.tbxInfoLimit.Location = New System.Drawing.Point(6, 18)
        Me.tbxInfoLimit.Name = "tbxInfoLimit"
        Me.tbxInfoLimit.Size = New System.Drawing.Size(43, 19)
        Me.tbxInfoLimit.TabIndex = 6
        Me.tbxInfoLimit.Text = "10"
        Me.tbxInfoLimit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(55, 21)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(17, 12)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "日"
        '
        'GroupBox4
        '
        Me.GroupBox4.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox4.Controls.Add(Me.tbxKmlUrl)
        Me.GroupBox4.Location = New System.Drawing.Point(6, 214)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(759, 50)
        Me.GroupBox4.TabIndex = 11
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "KMLフォルダのURL"
        '
        'tbxKmlUrl
        '
        Me.tbxKmlUrl.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbxKmlUrl.Location = New System.Drawing.Point(6, 18)
        Me.tbxKmlUrl.Name = "tbxKmlUrl"
        Me.tbxKmlUrl.Size = New System.Drawing.Size(676, 19)
        Me.tbxKmlUrl.TabIndex = 3
        '
        'GroupBox3
        '
        Me.GroupBox3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox3.Controls.Add(Me.lblHtmlFolder404)
        Me.GroupBox3.Controls.Add(Me.btnHtmlFolder)
        Me.GroupBox3.Controls.Add(Me.tbxHtmlFolder)
        Me.GroupBox3.Location = New System.Drawing.Point(6, 122)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(759, 60)
        Me.GroupBox3.TabIndex = 10
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Webページフォルダのパス"
        '
        'lblHtmlFolder404
        '
        Me.lblHtmlFolder404.AutoSize = True
        Me.lblHtmlFolder404.ForeColor = System.Drawing.Color.Red
        Me.lblHtmlFolder404.Location = New System.Drawing.Point(6, 40)
        Me.lblHtmlFolder404.Name = "lblHtmlFolder404"
        Me.lblHtmlFolder404.Size = New System.Drawing.Size(138, 12)
        Me.lblHtmlFolder404.TabIndex = 9
        Me.lblHtmlFolder404.Text = "※フォルダが見つかりません。"
        '
        'btnHtmlFolder
        '
        Me.btnHtmlFolder.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnHtmlFolder.Location = New System.Drawing.Point(690, 16)
        Me.btnHtmlFolder.Name = "btnHtmlFolder"
        Me.btnHtmlFolder.Size = New System.Drawing.Size(58, 23)
        Me.btnHtmlFolder.TabIndex = 5
        Me.btnHtmlFolder.Text = "参照"
        Me.btnHtmlFolder.UseVisualStyleBackColor = True
        '
        'tbxHtmlFolder
        '
        Me.tbxHtmlFolder.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbxHtmlFolder.Location = New System.Drawing.Point(8, 18)
        Me.tbxHtmlFolder.Name = "tbxHtmlFolder"
        Me.tbxHtmlFolder.Size = New System.Drawing.Size(676, 19)
        Me.tbxHtmlFolder.TabIndex = 3
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Controls.Add(Me.lblDataFolder404)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.btnDataFolder)
        Me.GroupBox2.Controls.Add(Me.tbxDataFolder)
        Me.GroupBox2.Location = New System.Drawing.Point(6, 17)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(759, 81)
        Me.GroupBox2.TabIndex = 9
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "dataフォルダのパス"
        '
        'lblDataFolder404
        '
        Me.lblDataFolder404.AutoSize = True
        Me.lblDataFolder404.ForeColor = System.Drawing.Color.Red
        Me.lblDataFolder404.Location = New System.Drawing.Point(6, 60)
        Me.lblDataFolder404.Name = "lblDataFolder404"
        Me.lblDataFolder404.Size = New System.Drawing.Size(138, 12)
        Me.lblDataFolder404.TabIndex = 7
        Me.lblDataFolder404.Text = "※フォルダが見つかりません。"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(6, 23)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(370, 12)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "※空欄の場合は、このプログラムファイルと同階層の「data」フォルダを探します。"
        '
        'btnDataFolder
        '
        Me.btnDataFolder.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDataFolder.Location = New System.Drawing.Point(690, 36)
        Me.btnDataFolder.Name = "btnDataFolder"
        Me.btnDataFolder.Size = New System.Drawing.Size(58, 23)
        Me.btnDataFolder.TabIndex = 5
        Me.btnDataFolder.Text = "参照"
        Me.btnDataFolder.UseVisualStyleBackColor = True
        '
        'tbxDataFolder
        '
        Me.tbxDataFolder.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbxDataFolder.Location = New System.Drawing.Point(8, 38)
        Me.tbxDataFolder.Name = "tbxDataFolder"
        Me.tbxDataFolder.Size = New System.Drawing.Size(676, 19)
        Me.tbxDataFolder.TabIndex = 3
        '
        'btnSaveSetup
        '
        Me.btnSaveSetup.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSaveSetup.Location = New System.Drawing.Point(621, 377)
        Me.btnSaveSetup.Name = "btnSaveSetup"
        Me.btnSaveSetup.Size = New System.Drawing.Size(133, 40)
        Me.btnSaveSetup.TabIndex = 8
        Me.btnSaveSetup.Text = "設定を保存する"
        Me.btnSaveSetup.UseVisualStyleBackColor = True
        '
        'tabLayersTxt
        '
        Me.tabLayersTxt.Controls.Add(Me.GroupBox5)
        Me.tabLayersTxt.Controls.Add(Me.txtUpdateMsg)
        Me.tabLayersTxt.Controls.Add(Me.btnUpdateHtml)
        Me.tabLayersTxt.Controls.Add(Me.btnSaveURL)
        Me.tabLayersTxt.Controls.Add(Me.dgvLayersUrl)
        Me.tabLayersTxt.Location = New System.Drawing.Point(4, 22)
        Me.tabLayersTxt.Name = "tabLayersTxt"
        Me.tabLayersTxt.Padding = New System.Windows.Forms.Padding(3)
        Me.tabLayersTxt.Size = New System.Drawing.Size(771, 430)
        Me.tabLayersTxt.TabIndex = 0
        Me.tabLayersTxt.Text = "KMLデータ作成"
        Me.tabLayersTxt.UseVisualStyleBackColor = True
        '
        'GroupBox5
        '
        Me.GroupBox5.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox5.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox5.Controls.Add(Me.Label7)
        Me.GroupBox5.Controls.Add(Me.Label6)
        Me.GroupBox5.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.GroupBox5.Location = New System.Drawing.Point(6, 16)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(757, 67)
        Me.GroupBox5.TabIndex = 18
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "※Webページの更新"
        '
        'Label7
        '
        Me.Label7.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label7.Location = New System.Drawing.Point(6, 37)
        Me.Label7.Name = "Label7"
        Me.Label7.Padding = New System.Windows.Forms.Padding(5)
        Me.Label7.Size = New System.Drawing.Size(745, 22)
        Me.Label7.TabIndex = 18
        Me.Label7.Text = "Webページの各KMLデータ「説明」欄には、「取得データ一覧」タブで保存されているテキストが使用されます。"
        '
        'Label6
        '
        Me.Label6.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label6.Location = New System.Drawing.Point(6, 15)
        Me.Label6.Name = "Label6"
        Me.Label6.Padding = New System.Windows.Forms.Padding(5)
        Me.Label6.Size = New System.Drawing.Size(745, 22)
        Me.Label6.TabIndex = 17
        Me.Label6.Text = "ここで保存された layers.txt 、およびファイル中に含まれるリンク先の layers.txt を元に、WebページとKMLファイルを更新します。" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Web" & _
            "ページの各KMLデータ「説明」欄には、「KMLデータ一覧」タブで保存されたテキストが使用されます。"
        '
        'txtUpdateMsg
        '
        Me.txtUpdateMsg.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtUpdateMsg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUpdateMsg.Location = New System.Drawing.Point(6, 359)
        Me.txtUpdateMsg.Multiline = True
        Me.txtUpdateMsg.Name = "txtUpdateMsg"
        Me.txtUpdateMsg.ReadOnly = True
        Me.txtUpdateMsg.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtUpdateMsg.Size = New System.Drawing.Size(635, 65)
        Me.txtUpdateMsg.TabIndex = 14
        Me.txtUpdateMsg.TabStop = False
        Me.txtUpdateMsg.Text = "（Webページ更新処理のログが表示されます）"
        '
        'btnUpdateHtml
        '
        Me.btnUpdateHtml.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnUpdateHtml.Location = New System.Drawing.Point(658, 392)
        Me.btnUpdateHtml.Name = "btnUpdateHtml"
        Me.btnUpdateHtml.Size = New System.Drawing.Size(105, 30)
        Me.btnUpdateHtml.TabIndex = 13
        Me.btnUpdateHtml.Text = "Webページを更新"
        Me.btnUpdateHtml.UseVisualStyleBackColor = True
        '
        'btnSaveURL
        '
        Me.btnSaveURL.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSaveURL.Location = New System.Drawing.Point(658, 359)
        Me.btnSaveURL.Name = "btnSaveURL"
        Me.btnSaveURL.Size = New System.Drawing.Size(105, 27)
        Me.btnSaveURL.TabIndex = 9
        Me.btnSaveURL.Text = "設定を保存"
        Me.btnSaveURL.UseVisualStyleBackColor = True
        '
        'dgvLayersUrl
        '
        Me.dgvLayersUrl.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvLayersUrl.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvLayersUrl.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.clmUrl, Me.clmUp, Me.clmDown})
        Me.dgvLayersUrl.Location = New System.Drawing.Point(6, 102)
        Me.dgvLayersUrl.MultiSelect = False
        Me.dgvLayersUrl.Name = "dgvLayersUrl"
        Me.dgvLayersUrl.RowHeadersWidth = 30
        Me.dgvLayersUrl.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.dgvLayersUrl.RowTemplate.Height = 21
        Me.dgvLayersUrl.Size = New System.Drawing.Size(757, 251)
        Me.dgvLayersUrl.TabIndex = 5
        '
        'clmUrl
        '
        Me.clmUrl.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.clmUrl.HeaderText = "layers.txtのURL"
        Me.clmUrl.Name = "clmUrl"
        Me.clmUrl.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.clmUrl.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'clmUp
        '
        Me.clmUp.HeaderText = ""
        Me.clmUp.Name = "clmUp"
        Me.clmUp.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.clmUp.Text = "↑"
        Me.clmUp.UseColumnTextForButtonValue = True
        Me.clmUp.Width = 40
        '
        'clmDown
        '
        Me.clmDown.HeaderText = ""
        Me.clmDown.Name = "clmDown"
        Me.clmDown.Text = "↓"
        Me.clmDown.UseColumnTextForButtonValue = True
        Me.clmDown.Width = 40
        '
        'tabLayersList
        '
        Me.tabLayersList.Controls.Add(Me.btnFilter)
        Me.tabLayersList.Controls.Add(Me.tbxFilter)
        Me.tabLayersList.Controls.Add(Me.btnSaveDesc)
        Me.tabLayersList.Controls.Add(Me.btnCountCancel)
        Me.tabLayersList.Controls.Add(Me.prbRead)
        Me.tabLayersList.Controls.Add(Me.grpCount)
        Me.tabLayersList.Controls.Add(Me.btnSaveCsv)
        Me.tabLayersList.Controls.Add(Me.dgvLayers)
        Me.tabLayersList.Location = New System.Drawing.Point(4, 22)
        Me.tabLayersList.Name = "tabLayersList"
        Me.tabLayersList.Padding = New System.Windows.Forms.Padding(3)
        Me.tabLayersList.Size = New System.Drawing.Size(771, 430)
        Me.tabLayersList.TabIndex = 1
        Me.tabLayersList.Text = "取得データ一覧"
        Me.tabLayersList.UseVisualStyleBackColor = True
        '
        'btnSaveDesc
        '
        Me.btnSaveDesc.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSaveDesc.Location = New System.Drawing.Point(475, 396)
        Me.btnSaveDesc.Name = "btnSaveDesc"
        Me.btnSaveDesc.Size = New System.Drawing.Size(140, 26)
        Me.btnSaveDesc.TabIndex = 19
        Me.btnSaveDesc.Text = "説明を保存"
        Me.btnSaveDesc.UseVisualStyleBackColor = True
        '
        'btnCountCancel
        '
        Me.btnCountCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnCountCancel.Enabled = False
        Me.btnCountCancel.Location = New System.Drawing.Point(211, 396)
        Me.btnCountCancel.Name = "btnCountCancel"
        Me.btnCountCancel.Size = New System.Drawing.Size(75, 26)
        Me.btnCountCancel.TabIndex = 12
        Me.btnCountCancel.Text = "集計中止"
        Me.btnCountCancel.UseVisualStyleBackColor = True
        '
        'prbRead
        '
        Me.prbRead.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.prbRead.Location = New System.Drawing.Point(6, 403)
        Me.prbRead.Name = "prbRead"
        Me.prbRead.Size = New System.Drawing.Size(199, 14)
        Me.prbRead.Step = 1
        Me.prbRead.TabIndex = 18
        '
        'grpCount
        '
        Me.grpCount.Controls.Add(Me.ckbFree)
        Me.grpCount.Controls.Add(Me.btnCount)
        Me.grpCount.Controls.Add(Me.lblEnd)
        Me.grpCount.Controls.Add(Me.lblStart)
        Me.grpCount.Controls.Add(Me.dtpEnd)
        Me.grpCount.Controls.Add(Me.rdbTotal)
        Me.grpCount.Controls.Add(Me.dtpStart)
        Me.grpCount.Controls.Add(Me.rdbDay)
        Me.grpCount.Controls.Add(Me.rdbMonth)
        Me.grpCount.Controls.Add(Me.rdbYear)
        Me.grpCount.Location = New System.Drawing.Point(8, 6)
        Me.grpCount.Name = "grpCount"
        Me.grpCount.Size = New System.Drawing.Size(700, 67)
        Me.grpCount.TabIndex = 17
        Me.grpCount.TabStop = False
        Me.grpCount.Text = "オプション"
        '
        'ckbFree
        '
        Me.ckbFree.AutoSize = True
        Me.ckbFree.Location = New System.Drawing.Point(272, 41)
        Me.ckbFree.Name = "ckbFree"
        Me.ckbFree.Size = New System.Drawing.Size(115, 16)
        Me.ckbFree.TabIndex = 11
        Me.ckbFree.Text = "任意の期間を選択"
        Me.ckbFree.UseVisualStyleBackColor = True
        '
        'btnCount
        '
        Me.btnCount.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCount.Location = New System.Drawing.Point(619, 15)
        Me.btnCount.Name = "btnCount"
        Me.btnCount.Size = New System.Drawing.Size(75, 23)
        Me.btnCount.TabIndex = 10
        Me.btnCount.Text = "集計開始"
        Me.btnCount.UseVisualStyleBackColor = True
        '
        'lblEnd
        '
        Me.lblEnd.AutoSize = True
        Me.lblEnd.Location = New System.Drawing.Point(580, 22)
        Me.lblEnd.Name = "lblEnd"
        Me.lblEnd.Size = New System.Drawing.Size(24, 12)
        Me.lblEnd.TabIndex = 9
        Me.lblEnd.Text = "まで"
        '
        'lblStart
        '
        Me.lblStart.AutoSize = True
        Me.lblStart.Location = New System.Drawing.Point(411, 22)
        Me.lblStart.Name = "lblStart"
        Me.lblStart.Size = New System.Drawing.Size(23, 12)
        Me.lblStart.TabIndex = 8
        Me.lblStart.Text = "から"
        '
        'dtpEnd
        '
        Me.dtpEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpEnd.Location = New System.Drawing.Point(440, 16)
        Me.dtpEnd.Name = "dtpEnd"
        Me.dtpEnd.Size = New System.Drawing.Size(135, 19)
        Me.dtpEnd.TabIndex = 7
        '
        'rdbTotal
        '
        Me.rdbTotal.AutoSize = True
        Me.rdbTotal.Location = New System.Drawing.Point(199, 18)
        Me.rdbTotal.Name = "rdbTotal"
        Me.rdbTotal.Size = New System.Drawing.Size(47, 16)
        Me.rdbTotal.TabIndex = 6
        Me.rdbTotal.Text = "通算"
        Me.rdbTotal.UseVisualStyleBackColor = True
        '
        'dtpStart
        '
        Me.dtpStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpStart.Location = New System.Drawing.Point(272, 16)
        Me.dtpStart.Name = "dtpStart"
        Me.dtpStart.Size = New System.Drawing.Size(135, 19)
        Me.dtpStart.TabIndex = 5
        '
        'rdbDay
        '
        Me.rdbDay.AutoSize = True
        Me.rdbDay.Checked = True
        Me.rdbDay.Location = New System.Drawing.Point(6, 18)
        Me.rdbDay.Name = "rdbDay"
        Me.rdbDay.Size = New System.Drawing.Size(59, 16)
        Me.rdbDay.TabIndex = 2
        Me.rdbDay.TabStop = True
        Me.rdbDay.Text = "日単位"
        Me.rdbDay.UseVisualStyleBackColor = True
        '
        'rdbMonth
        '
        Me.rdbMonth.AutoSize = True
        Me.rdbMonth.Location = New System.Drawing.Point(69, 18)
        Me.rdbMonth.Name = "rdbMonth"
        Me.rdbMonth.Size = New System.Drawing.Size(59, 16)
        Me.rdbMonth.TabIndex = 1
        Me.rdbMonth.Text = "月単位"
        Me.rdbMonth.UseVisualStyleBackColor = True
        '
        'rdbYear
        '
        Me.rdbYear.AutoSize = True
        Me.rdbYear.Location = New System.Drawing.Point(134, 18)
        Me.rdbYear.Name = "rdbYear"
        Me.rdbYear.Size = New System.Drawing.Size(59, 16)
        Me.rdbYear.TabIndex = 0
        Me.rdbYear.Text = "年単位"
        Me.rdbYear.UseVisualStyleBackColor = True
        '
        'btnSaveCsv
        '
        Me.btnSaveCsv.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSaveCsv.Location = New System.Drawing.Point(625, 396)
        Me.btnSaveCsv.Name = "btnSaveCsv"
        Me.btnSaveCsv.Size = New System.Drawing.Size(140, 26)
        Me.btnSaveCsv.TabIndex = 12
        Me.btnSaveCsv.Text = "csvファイルとして保存"
        Me.btnSaveCsv.UseVisualStyleBackColor = True
        '
        'dgvLayers
        '
        Me.dgvLayers.AllowUserToAddRows = False
        Me.dgvLayers.AllowUserToDeleteRows = False
        Me.dgvLayers.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvLayers.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ColumnReadOrder, Me.ColumnLayerID, Me.ColumnFileName, Me.ColumnDescription, Me.ColumnAccessCount})
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle6.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvLayers.DefaultCellStyle = DataGridViewCellStyle6
        Me.dgvLayers.Location = New System.Drawing.Point(8, 105)
        Me.dgvLayers.MultiSelect = False
        Me.dgvLayers.Name = "dgvLayers"
        Me.dgvLayers.RowHeadersWidth = 30
        Me.dgvLayers.RowTemplate.Height = 21
        Me.dgvLayers.Size = New System.Drawing.Size(757, 287)
        Me.dgvLayers.TabIndex = 11
        '
        'ColumnReadOrder
        '
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.ColumnReadOrder.DefaultCellStyle = DataGridViewCellStyle1
        Me.ColumnReadOrder.FillWeight = 70.0!
        Me.ColumnReadOrder.HeaderText = "読込順"
        Me.ColumnReadOrder.MinimumWidth = 70
        Me.ColumnReadOrder.Name = "ColumnReadOrder"
        Me.ColumnReadOrder.ReadOnly = True
        Me.ColumnReadOrder.Width = 70
        '
        'ColumnLayerID
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        Me.ColumnLayerID.DefaultCellStyle = DataGridViewCellStyle2
        Me.ColumnLayerID.HeaderText = "LayerID"
        Me.ColumnLayerID.MinimumWidth = 50
        Me.ColumnLayerID.Name = "ColumnLayerID"
        Me.ColumnLayerID.ReadOnly = True
        Me.ColumnLayerID.Visible = False
        '
        'ColumnFileName
        '
        Me.ColumnFileName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        Me.ColumnFileName.DefaultCellStyle = DataGridViewCellStyle3
        Me.ColumnFileName.FillWeight = 150.0!
        Me.ColumnFileName.HeaderText = "KMLファイル名"
        Me.ColumnFileName.MinimumWidth = 150
        Me.ColumnFileName.Name = "ColumnFileName"
        Me.ColumnFileName.ReadOnly = True
        '
        'ColumnDescription
        '
        Me.ColumnDescription.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.ControlLightLight
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.ColumnDescription.DefaultCellStyle = DataGridViewCellStyle4
        Me.ColumnDescription.FillWeight = 150.0!
        Me.ColumnDescription.HeaderText = "説明"
        Me.ColumnDescription.MinimumWidth = 150
        Me.ColumnDescription.Name = "ColumnDescription"
        '
        'ColumnAccessCount
        '
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.ColumnAccessCount.DefaultCellStyle = DataGridViewCellStyle5
        Me.ColumnAccessCount.FillWeight = 80.0!
        Me.ColumnAccessCount.HeaderText = "合計"
        Me.ColumnAccessCount.MinimumWidth = 80
        Me.ColumnAccessCount.Name = "ColumnAccessCount"
        Me.ColumnAccessCount.ReadOnly = True
        Me.ColumnAccessCount.Width = 80
        '
        'DataSet1
        '
        Me.DataSet1.DataSetName = "GsiKml"
        Me.DataSet1.Tables.AddRange(New System.Data.DataTable() {Me.dtsConf})
        '
        'dtsConf
        '
        Me.dtsConf.Columns.AddRange(New System.Data.DataColumn() {Me.DataColumn1, Me.DataColumn2, Me.DataColumn3, Me.DataColumn4})
        Me.dtsConf.TableName = "Conf"
        '
        'DataColumn1
        '
        Me.DataColumn1.ColumnName = "DataFolderPath"
        Me.DataColumn1.DefaultValue = ""
        '
        'DataColumn2
        '
        Me.DataColumn2.ColumnName = "HtmlFolderPath"
        Me.DataColumn2.DefaultValue = ""
        '
        'DataColumn3
        '
        Me.DataColumn3.ColumnName = "KmlFolderUrl"
        Me.DataColumn3.DefaultValue = ""
        '
        'DataColumn4
        '
        Me.DataColumn4.ColumnName = "InfoLimit"
        '
        'BackgroundWorker1
        '
        Me.BackgroundWorker1.WorkerReportsProgress = True
        Me.BackgroundWorker1.WorkerSupportsCancellation = True
        '
        'BackgroundWorker2
        '
        '
        'tbxFilter
        '
        Me.tbxFilter.Location = New System.Drawing.Point(14, 80)
        Me.tbxFilter.Name = "tbxFilter"
        Me.tbxFilter.Size = New System.Drawing.Size(401, 19)
        Me.tbxFilter.TabIndex = 20
        '
        'btnFilter
        '
        Me.btnFilter.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnFilter.Location = New System.Drawing.Point(421, 79)
        Me.btnFilter.Name = "btnFilter"
        Me.btnFilter.Size = New System.Drawing.Size(75, 23)
        Me.btnFilter.TabIndex = 21
        Me.btnFilter.Text = "絞込検索"
        Me.btnFilter.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(784, 462)
        Me.Controls.Add(Me.tabControl1)
        Me.Name = "Form1"
        Me.Text = "GsiKmlNetworkLinkManager"
        Me.tabControl1.ResumeLayout(False)
        Me.tabSetup.ResumeLayout(False)
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox6.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.tabLayersTxt.ResumeLayout(False)
        Me.tabLayersTxt.PerformLayout()
        Me.GroupBox5.ResumeLayout(False)
        CType(Me.dgvLayersUrl, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabLayersList.ResumeLayout(False)
        Me.tabLayersList.PerformLayout()
        Me.grpCount.ResumeLayout(False)
        Me.grpCount.PerformLayout()
        CType(Me.dgvLayers, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataSet1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtsConf, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents tabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents tabLayersTxt As System.Windows.Forms.TabPage
    Friend WithEvents tabLayersList As System.Windows.Forms.TabPage
    Private WithEvents dgvLayersUrl As System.Windows.Forms.DataGridView
    Friend WithEvents btnUpdateHtml As System.Windows.Forms.Button
    Private WithEvents btnSaveURL As System.Windows.Forms.Button
    Friend WithEvents btnSaveCsv As System.Windows.Forms.Button
    Friend WithEvents dgvLayers As System.Windows.Forms.DataGridView
    Friend WithEvents grpCount As System.Windows.Forms.GroupBox
    Friend WithEvents rdbDay As System.Windows.Forms.RadioButton
    Friend WithEvents rdbMonth As System.Windows.Forms.RadioButton
    Friend WithEvents rdbYear As System.Windows.Forms.RadioButton
    Friend WithEvents rdbTotal As System.Windows.Forms.RadioButton
    Friend WithEvents prbRead As System.Windows.Forms.ProgressBar
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents btnCountCancel As System.Windows.Forms.Button
    Friend WithEvents btnSaveDesc As System.Windows.Forms.Button
    Friend WithEvents txtUpdateMsg As System.Windows.Forms.TextBox
    Friend WithEvents BackgroundWorker2 As System.ComponentModel.BackgroundWorker
    Friend WithEvents tabSetup As System.Windows.Forms.TabPage
    Friend WithEvents DataSet1 As System.Data.DataSet
    Friend WithEvents dtsConf As System.Data.DataTable
    Friend WithEvents btnSaveSetup As System.Windows.Forms.Button
    Friend WithEvents DataColumn1 As System.Data.DataColumn
    Friend WithEvents DataColumn2 As System.Data.DataColumn
    Friend WithEvents DataColumn3 As System.Data.DataColumn
    Friend WithEvents FolderBrowserDialog1 As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents tbxKmlUrl As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents lblHtmlFolder404 As System.Windows.Forms.Label
    Friend WithEvents btnHtmlFolder As System.Windows.Forms.Button
    Friend WithEvents tbxHtmlFolder As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents lblDataFolder404 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents btnDataFolder As System.Windows.Forms.Button
    Friend WithEvents tbxDataFolder As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox6 As System.Windows.Forms.GroupBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents DataColumn4 As System.Data.DataColumn
    Friend WithEvents clmUrl As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents clmUp As System.Windows.Forms.DataGridViewButtonColumn
    Friend WithEvents clmDown As System.Windows.Forms.DataGridViewButtonColumn
    Friend WithEvents btnCount As System.Windows.Forms.Button
    Friend WithEvents lblEnd As System.Windows.Forms.Label
    Friend WithEvents lblStart As System.Windows.Forms.Label
    Friend WithEvents dtpEnd As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpStart As System.Windows.Forms.DateTimePicker
    Friend WithEvents ColumnReadOrder As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColumnLayerID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColumnFileName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColumnDescription As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColumnAccessCount As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ckbFree As System.Windows.Forms.CheckBox
    Friend WithEvents tbxInfoLimit As System.Windows.Forms.TextBox
    Friend WithEvents btnFilter As System.Windows.Forms.Button
    Friend WithEvents tbxFilter As System.Windows.Forms.TextBox

End Class
