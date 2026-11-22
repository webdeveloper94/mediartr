with open('MediChat.xaml', 'r', encoding='utf-8') as f:
    text = f.read()

target = '<TextBlock x:Name="Status" Text="Savolingizni yozing." Foreground="#99F6E4" FontSize="10" TextWrapping="Wrap" Margin="0,8" AutomationProperties.LiveSetting="Polite"/>'

replacement = '''<!-- Thinking Indicator Box (Kutish vaqtida ko‘zga tashlanadigan animatsion blok) -->
   <Border x:Name="ThinkingBox" Visibility="Collapsed" Margin="0,4,0,8"
           Background="#1C3848" BorderBrush="#2DD4BF" BorderThickness="1.5" CornerRadius="12" Padding="12,9">
    <DockPanel>
     <Border DockPanel.Dock="Left" Width="28" Height="28" CornerRadius="14" Background="#132B3A" Margin="0,0,10,0">
      <TextBlock Text="🤖" FontSize="16" HorizontalAlignment="Center" VerticalAlignment="Center"/>
     </Border>
     <StackPanel VerticalAlignment="Center">
      <StackPanel Orientation="Horizontal">
       <TextBlock Text="O‘ylayapman, kutib turing" Foreground="#5EEAD4" FontSize="12" FontWeight="Bold"/>
       <TextBlock x:Name="ThinkingDots" Text="•••" Foreground="#5EEAD4" FontSize="12" FontWeight="Bold" Margin="4,0,0,0"/>
      </StackPanel>
      <TextBlock Text="Mavzu manbalari tahlil qilinmoqda…" Foreground="#94A3B8" FontSize="10" Margin="0,1,0,0"/>
     </StackPanel>
    </DockPanel>
   </Border>

   <TextBlock x:Name="Status" Text="Savolingizni yozing." Foreground="#99F6E4" FontSize="11" TextWrapping="Wrap" Margin="0,4,0,8" AutomationProperties.LiveSetting="Polite"/>'''

assert target in text, "target not found"
text = text.replace(target, replacement)

with open('MediChat.xaml', 'w', encoding='utf-8') as f:
    f.write(text)

print("MediChat.xaml updated with ThinkingBox!")
