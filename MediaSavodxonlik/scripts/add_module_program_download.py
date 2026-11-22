with open('MainWindow.xaml', 'r', encoding='utf-8') as f:
    text = f.read()

target = '''       <TextBlock x:Name="SectionBody" TextWrapping="Wrap" Foreground="#BAC8D9" FontSize="14" LineHeight="26"/>
      </StackPanel></Border>'''

replacement = '''       <TextBlock x:Name="SectionBody" TextWrapping="Wrap" Foreground="#BAC8D9" FontSize="14" LineHeight="26"/>
       <!-- Modul dasturi PDF yuklab olish bloki -->
       <Border x:Name="ModuleProgramCard" Margin="0,22,0,0" Background="#1B2F44" BorderBrush="#38BDF8" BorderThickness="1.5" CornerRadius="14" Padding="20">
        <DockPanel LastChildFill="True">
         <Button x:Name="DownloadModuleDocButton" DockPanel.Dock="Right" Click="DownloadModuleProgram"
                 Background="#164E63" Foreground="#38BDF8" FontWeight="SemiBold" FontSize="14"
                 Padding="18,12" BorderThickness="1" BorderBrush="#0284C7" Cursor="Hand"
                 VerticalAlignment="Center" Margin="16,0,0,0" ToolTip="Modul o‘quv dasturini PDF formatida yuklab olish">
          <Button.Resources>
           <Style TargetType="Border">
            <Setter Property="CornerRadius" Value="10"/>
           </Style>
          </Button.Resources>
          <StackPanel Orientation="Horizontal">
           <TextBlock Text="⤓" FontSize="17" FontWeight="Bold" Margin="0,-2,8,0" VerticalAlignment="Center"/>
           <TextBlock Text="Modul dasturini yuklash (.pdf)" VerticalAlignment="Center"/>
          </StackPanel>
         </Button>
         <Border Width="48" Height="48" CornerRadius="12" Background="#24384D" Margin="0,0,16,0" VerticalAlignment="Center">
          <TextBlock Text="📄" FontSize="24" HorizontalAlignment="Center" VerticalAlignment="Center"/>
         </Border>
         <StackPanel VerticalAlignment="Center">
          <TextBlock Text="Modulning rasmiy o‘quv dasturi (PDF)" FontSize="16" FontWeight="Bold" Foreground="#F1F5F9"/>
          <TextBlock Text="Media va axborot savodxonligi fani bo‘yicha to‘liq ta’lim standarti, mavzular rejasi va adabiyotlar ro‘yxati."
                     FontSize="12" Foreground="#94A3B8" TextWrapping="Wrap" Margin="0,4,0,0"/>
         </StackPanel>
        </DockPanel>
       </Border>
      </StackPanel></Border>'''

assert target in text, "target not found in MainWindow.xaml"
text = text.replace(target, replacement)

with open('MainWindow.xaml', 'w', encoding='utf-8') as f:
    f.write(text)

print("MainWindow.xaml updated with ModuleProgramCard!")
