import re

with open('MainWindow.xaml', 'r', encoding='utf-8') as f:
    text = f.read()

# 1. Update navigation: add AboutNav before AuthorNav
old_nav = '''     <Button x:Name="GlossaryNav" Style="{StaticResource Nav}" Tag="glossary" Click="Navigate"><TextBlock Text="Aa   Glossariy"/></Button>
     <Border Height="1" Background="#233047" Margin="14,22"/>
     <Button x:Name="AuthorNav" Style="{StaticResource Nav}" Tag="author" Click="Navigate"><TextBlock Text="ⓘ    Muallif"/></Button>'''

new_nav = '''     <Button x:Name="GlossaryNav" Style="{StaticResource Nav}" Tag="glossary" Click="Navigate"><TextBlock Text="Aa   Glossariy"/></Button>
     <Button x:Name="AboutNav" Style="{StaticResource Nav}" Tag="about" Click="Navigate"><TextBlock Text="ⓘ    Modul haqida"/></Button>
     <Border Height="1" Background="#233047" Margin="14,22"/>
     <Button x:Name="AuthorNav" Style="{StaticResource Nav}" Tag="author" Click="Navigate"><TextBlock Text="👤   Muallif"/></Button>'''

if old_nav in text:
    text = text.replace(old_nav, new_nav)
    print("Navigation updated successfully!")
else:
    print("Warning: old_nav not matched!")

# 2. Update Hero banner button
old_btn = '<StackPanel Orientation="Horizontal"><Button Background="#2DD4BF" Foreground="#082C30" FontWeight="SemiBold" Padding="18,12" Tag="topics" Click="Navigate" Content="Mavzular · 20 ta   →"/><Button Margin="9,0,0,0" Padding="14,12" BorderBrush="#3C5367" BorderThickness="1" Tag="about" Click="Navigate" Content="Modul haqida"/></StackPanel>'

new_btn = '''<Button x:Name="StartTopicsButton" Tag="topics" Click="Navigate"
                 Margin="0,8,0,0" Padding="0" BorderThickness="0" Background="Transparent"
                 HorizontalAlignment="Left" Cursor="Hand" AutomationProperties.Name="O‘quv mavzulariga o‘tish">
          <Button.RenderTransform>
           <TranslateTransform Y="0"/>
          </Button.RenderTransform>
          <Button.Style>
           <Style TargetType="Button">
            <Setter Property="RenderTransform"><Setter.Value><TranslateTransform/></Setter.Value></Setter>
            <Style.Triggers>
             <Trigger Property="IsMouseOver" Value="True">
              <Setter Property="Cursor" Value="Hand"/>
             </Trigger>
            </Style.Triggers>
           </Style>
          </Button.Style>
          <Border x:Name="StartTopicsCard" CornerRadius="16" Padding="24,15" BorderThickness="1.5">
           <Border.Background>
            <LinearGradientBrush StartPoint="0,0" EndPoint="1,1">
             <GradientStop Color="#38E1CB" Offset="0"/>
             <GradientStop Color="#14B8A6" Offset="0.55"/>
             <GradientStop Color="#0D9488" Offset="1"/>
            </LinearGradientBrush>
           </Border.Background>
           <Border.BorderBrush>
            <LinearGradientBrush StartPoint="0,0" EndPoint="1,1">
             <GradientStop Color="#A7F3D0" Offset="0"/>
             <GradientStop Color="#2DD4BF" Offset="1"/>
            </LinearGradientBrush>
           </Border.BorderBrush>
           <Border.Effect>
            <DropShadowEffect Color="#2DD4BF" BlurRadius="28" ShadowDepth="4" Opacity="0.45"/>
           </Border.Effect>
           <DockPanel>
            <Border DockPanel.Dock="Right" Width="40" Height="40" CornerRadius="12"
                    Background="#22042F2E" VerticalAlignment="Center" Margin="20,0,0,0">
             <TextBlock Text="➔" FontSize="19" FontWeight="Black" Foreground="#042F2E"
                        HorizontalAlignment="Center" VerticalAlignment="Center" Margin="2,-1,0,0"/>
            </Border>
            <StackPanel VerticalAlignment="Center">
             <TextBlock Text="O‘QUV KURSI BO‘YICHA" FontSize="10" FontWeight="Bold"
                        Foreground="#064E3B" LetterSpacing="1" Margin="0,0,0,3"/>
             <TextBlock Text="Mavzularni o‘rganishni boshlash (20 ta)" FontSize="17" FontWeight="Bold"
                        Foreground="#042F2E" TextWrapping="Wrap"/>
            </StackPanel>
           </DockPanel>
          </Border>
         </Button>'''

if old_btn in text:
    text = text.replace(old_btn, new_btn)
    print("Hero button updated successfully!")
else:
    print("Warning: old_btn not matched!")

with open('MainWindow.xaml', 'w', encoding='utf-8') as f:
    f.write(text)

print("MainWindow.xaml written successfully.")
