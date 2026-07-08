interface TitleBarButtonProps {
  onClick?: () => void;
  icon: React.ReactNode;
  variant?: 'default' | 'danger';
}

export function TitleBarButton({ onClick, icon, variant = 'default' }: TitleBarButtonProps) {
  return (
    <button
      onClick={onClick}
      className={`p-2 hover:bg-black/5 rounded transition-colors ${
        variant === 'danger' ? 'hover:bg-red-500/10 text-red-600' : 'text-gray-700'
      }`}
    >
      {icon}
    </button>
  );
}
